using System.Collections.Generic;
using System.IO;
using System.Reactive.Subjects;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace WMS.data.repository;

public abstract class SerializationRepository<T>
{
    private string _filePath;
    private Stream? _stream;
    private protected BehaviorSubject<List<T>> EntitiesSubject { get; }
    
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

    private List<T> Deserialize()
    {
        _stream = GetStream();
        var list = JsonSerializer.Deserialize<List<T>>(_stream, _options);
        _stream.Close();
        
        return list;
    }

    protected SerializationRepository(string filePath)
    {
        _filePath = filePath;
    }

    protected void Append(T obj)
    {
        var list = Deserialize();
        list.Add(obj);
        Serialize(list);
    }

    protected void Remove(T obj)
    {
        var list = Deserialize();
        list.Remove(obj);
        Serialize(list);
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