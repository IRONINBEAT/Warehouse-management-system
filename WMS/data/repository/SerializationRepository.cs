using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using DynamicData;
using WMS.domain.entity;

namespace WMS.data.repository;

public abstract class SerializationRepository<T>
{
    private string _filePath;
    private Stream? _stream;
    
    protected SerializationRepository(string filePath)
    {
        _filePath = filePath;
        EntitiesSubject = new BehaviorSubject<List<T>>(new List<T>());
        AsObservable = EntitiesSubject.AsObservable();
    }
    
    private BehaviorSubject<List<T>> EntitiesSubject { get; }
    protected IObservable<List<T>> AsObservable { get; }
    
    public abstract bool CompareEntities(T obj1, T obj2);
    
    private readonly JsonSerializerOptions _options = new()
    {
        WriteIndented = true,
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic)
    };
    
    private void Serialize(List<T>? obj)
    {
        if (obj == null) return;
        _stream = GetStream();
        _stream.SetLength(0);
        _stream.Flush();
        JsonSerializer.SerializeAsync(_stream, obj, _options);
        _stream.Close();
        EntitiesSubject.OnNext(obj);
    }

    protected List<T> Deserialize()
    {
        List<T>? deserialized = null;
        try
        {
            _stream = GetStream();
            deserialized = JsonSerializer.Deserialize<List<T>>(_stream, _options);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        finally
        {
            _stream?.Close();
            deserialized ??= new List<T>();
        }

        return deserialized;
    }

   
    protected void Append(T obj)
    {
        var list = Deserialize();
        list.Add(obj);
        Serialize(list);
    }

    protected void RemoveItem(T obj)
    {
        var newEntities = Deserialize();
        var oldEntities = Deserialize();
        foreach (var entity in oldEntities)
        {
            if (!CompareEntities(obj, entity)) continue;
            for (var i = 0; i < oldEntities.Count; i++)
                if (CompareEntities(oldEntities[i], obj))
                {
                    newEntities.RemoveAt(i);
                    break;
                }

            EntitiesSubject.OnNext(newEntities);
            Serialize(newEntities);
            break;
        }
    }

    protected void Change(T obj)
    {
        var newObj = Deserialize();
        var oldObj = Deserialize();
        foreach (var o in oldObj)
        {
            if (!CompareEntities(o, obj)) continue;
            for (var i = 0; i < oldObj.Count; i++)
            {
                if (CompareEntities(o, obj))
                {
                    newObj.RemoveAt(i);
                    break;
                }
                newObj.Add(obj);
                Serialize(newObj);
                break;
            }
        }
    }
    
    
    private Stream GetStream()
    {
        return new FileStream
        (
            _filePath,
            FileMode.OpenOrCreate,
            FileAccess.ReadWrite,
            FileShare.ReadWrite,
            4096,
            FileOptions.None
        );
    }
}