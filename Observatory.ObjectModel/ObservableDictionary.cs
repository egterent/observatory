using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.Specialized;
using System.Collections;

namespace Observatory.ObjectModel
{
    public class ObservableDictionary<TKey, TValue> : IDictionary<TKey, TValue>, 
        ICollection<KeyValuePair<TKey,TValue>>, 
        IEnumerable<KeyValuePair<TKey, TValue>>,
        IDictionary, 
        ICollection,
        IEnumerable,
        INotifyCollectionChanged, INotifyPropertyChanged
    {
        private IDictionary<TKey, TValue> _dictionary;

        public ObservableDictionary()
        {
            _dictionary = new Dictionary<TKey, TValue>();
        }
        
        public ObservableDictionary(IDictionary<TKey, TValue> dictionary)
        {
            _dictionary = new Dictionary<TKey, TValue>(dictionary);
        }

        public ObservableDictionary(IEqualityComparer<TKey> comparer)
        {
            _dictionary = new Dictionary<TKey, TValue>(comparer);
        }

        public ObservableDictionary(int capacity)
        {
            _dictionary = new Dictionary<TKey, TValue>(capacity);
        }

        public ObservableDictionary(IDictionary<TKey, TValue> dictionary, 
            IEqualityComparer<TKey> comparer)
        {
            _dictionary = new Dictionary<TKey, TValue>(dictionary, comparer);
        }

        public ObservableDictionary(int capacity, IEqualityComparer<TKey> comparer)
        {
            _dictionary = new Dictionary<TKey, TValue>(capacity, comparer);
        }

        public ICollection<TKey> Keys
        {
            get { return _dictionary.Keys; }
        }

        public ICollection<TValue> Values
        {
            get { return _dictionary.Values; }
        }

        public int Count
        {
            get { return _dictionary.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool IsFixedSize
        {
            get
            {
                return false;
            }
        }

        public object SyncRoot
        {
            get { return this; }
        }

        public bool IsSynchronized
        {
            get { return false; }
        }

        ICollection IDictionary.Keys
        {
            get { return (ICollection)_dictionary.Keys; }
        }

        ICollection IDictionary.Values
        { get { return (ICollection)_dictionary.Values; } }

        public object this[object key]
        {
            get { return _dictionary[(TKey)key]; }

            set { this[(TKey)key] = (TValue)value; }
        }

        public TValue this[TKey key]
        {
            get { return _dictionary[key]; }

            set
            {
                if (key == null || value == null)
                    throw new ArgumentNullException();
                TValue item;
                if (_dictionary.TryGetValue(key, out item))
                {
                    if (Equals(value, item))
                        return;
                    _dictionary[key] = value;
                    OnReplaceItem(new KeyValuePair<TKey, TValue>(key, value),
                        new KeyValuePair<TKey, TValue>(key, item));
                }
                else
                    Add(key, value);                
            }
        }

        public bool ContainsKey(TKey key)
        {
            return _dictionary.ContainsKey(key);
        }

        public bool Remove(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException();
            TValue value;
            _dictionary.TryGetValue(key, out value);
            var removed = _dictionary.Remove(key);
            if (removed)
                OnRemoveItem(new KeyValuePair<TKey, TValue>(key, value));
            if (_dictionary.Count == 0)
                OnResetCollection();
            return removed;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return _dictionary.TryGetValue(key, out value);
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        public void Clear()
        {
            _dictionary.Clear();
            OnResetCollection();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            TValue value;
            return _dictionary.TryGetValue(item.Key, out value) 
                && value.Equals(item.Value);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            _dictionary.CopyTo(array, arrayIndex); 
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return Remove(item.Key);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(TKey key, TValue value)
        {
            if (key == null || value == null)
                throw new ArgumentNullException();
            if (_dictionary.ContainsKey(key))
                throw new ArgumentException
                    ("Объект с данным ключем уже присутствует в коллекции");
            _dictionary.Add(key, value);
            OnAddNewItem(new KeyValuePair<TKey, TValue>(key, value));
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnPropertyChanged()
        {
            OnPropertyChanged("Count");
            OnPropertyChanged("Item[]");
            OnPropertyChanged("Keys");
            OnPropertyChanged("Values");
        }

        public void OnAddNewItem(KeyValuePair<TKey, TValue> chengedItem)
        {
            OnPropertyChanged();
            if (CollectionChanged != null)
                CollectionChanged(this, new NotifyCollectionChangedEventArgs
                    (NotifyCollectionChangedAction.Add, chengedItem));
        }

        public void OnReplaceItem(KeyValuePair<TKey, TValue> newItem, 
            KeyValuePair<TKey, TValue> oldItem)
        {
            OnPropertyChanged();
            if (CollectionChanged != null)
                CollectionChanged(this, new NotifyCollectionChangedEventArgs
                    (NotifyCollectionChangedAction.Replace, newItem, oldItem));
        }

        public void OnRemoveItem(KeyValuePair<TKey, TValue> chengedItem)
        {
            OnPropertyChanged();
            if (CollectionChanged != null)
                CollectionChanged(this, new NotifyCollectionChangedEventArgs
                    (NotifyCollectionChangedAction.Remove, chengedItem));
        }

        public void OnResetCollection()
        {
            OnPropertyChanged();
            if (CollectionChanged != null)
                CollectionChanged(this, new NotifyCollectionChangedEventArgs
                    (NotifyCollectionChangedAction.Reset));
        }

        public bool Contains(object key)
        {
            return _dictionary.ContainsKey((TKey)key);
        }

        public void Add(object key, object value)
        {
            Add((TKey)key, (TValue)value);
        }

        IDictionaryEnumerator IDictionary.GetEnumerator()
        {
            return (IDictionaryEnumerator)GetEnumerator();
        }

        public void Remove(object key)
        {
            Remove((TKey)key);
        }

        public void CopyTo(Array array, int index)
        {
            _dictionary.CopyTo((KeyValuePair<TKey,TValue>[])array, index);
        }
    }
}
