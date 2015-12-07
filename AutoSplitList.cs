using LiveSplit.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml;

namespace LiveSplit.AutoSplitting
{
	public class AutoSplitList : IList<AutoSplit>
	{
		public const string DEFAULT_XML_NAME = "AutoSplitList";

		public string Name { get; set; }

		public int Count => _list.Count;

		public bool IsReadOnly => false;

		List<AutoSplit> _list;
		Dictionary<GameEvent, IList<AutoSplit>> _dictionary;

		public AutoSplitList(string name = "")
		{
			Name = name;
			_list = new List<AutoSplit>();
			_dictionary = new Dictionary<GameEvent, IList<AutoSplit>>();
		}

		public XmlElement ToXml(XmlDocument doc, string name = DEFAULT_XML_NAME)
		{
			var root = doc.CreateElement(name);
			root.AppendChild(SettingsHelper.ToElement(doc, nameof(Name), Name));
			var autosplitsNode = root.AppendChild(doc.CreateElement("AutoSplits"));
			foreach (var split in this)
				autosplitsNode.AppendChild(split.ToXml(doc));
			return root;
		}

		public static AutoSplitList FromXml(XmlElement elem, AutoSplitEnv env)
		{
			var list = new AutoSplitList(elem[nameof(Name)]?.InnerText ?? "");
			foreach (XmlElement child in elem["AutoSplits"])
				list.Add(AutoSplit.FromXml(child, env));
			return list;
		}

		public static bool IsValidXml(XmlElement elem)
		{
			if (elem == null || elem["AutoSplits"] == null)
				return false;

			foreach (XmlElement child in elem["AutoSplits"])
			{
				if (!AutoSplit.IsValidXml(child))
					return false;
			}

			return true;
		}

		public static AutoSplitList[] ArrayFromXml(XmlElement elem, AutoSplitEnv env)
		{
			var list = new List<AutoSplitList>();
			foreach (XmlElement child in elem.ChildNodes)
				list.Add(FromXml(child, env));
			return list.ToArray();
		}

		public IEnumerable<AutoSplit> this[GameEvent evnt]
		{
			get
			{
				IList<AutoSplit> list;
				if (_dictionary.TryGetValue(evnt, out list))
					return list;
				else
					return new AutoSplit[0];
			}
		}

		public AutoSplit this[int index]
		{
			get { return _list[index]; }
			set
			{
				Remove(_list[index]);
				Insert(index, value);
			}
		}

		void CreateKey(GameEvent evnt)
		{
			if (!_dictionary.ContainsKey(evnt))
				_dictionary.Add(evnt, new List<AutoSplit>());
		}

		public void Add(AutoSplit split)
		{
			if (split == null)
				throw new ArgumentNullException("split");

			if (Contains(split))
				throw new ArgumentException("AutoSplit already in the list");

			_list.Add(split);
			CreateKey(split.Event);
			_dictionary[split.Event].Add(split);
			split.PropertyChanged += Split_PropertyChanged;
		}

		void Split_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			var split = (AutoSplit)sender;
			if (e.PropertyName != nameof(split.Event))
				return;

			foreach (var pair in _dictionary)
			{
				var eventList = pair.Value;
				if (eventList.Contains(split))
				{
					if (pair.Key != split.Event)
					{
						eventList.Remove(split);
						CreateKey(split.Event);
						_dictionary[split.Event].Add(split);
					}
					break;
				}
			}
		}

		public void AddRange(IEnumerable<AutoSplit> list)
		{
			foreach (var item in list)
				Add(item);
		}

		public AutoSplit Find(string name) => this.First(split => split.Name == name);

		/// <summary>
		/// Returns a deep copy of the current <see cref="AutoSplitList"/>.
		/// </summary>
		public AutoSplitList Clone(AutoSplitEnv env)
		{
			var clone = new AutoSplitList(Name);
			clone.AddRange(this.Select(a => a.Clone(env)));
			return clone;
		}

		public void Insert(int index, AutoSplit item)
		{
			_list.Insert(index, item);
			CreateKey(item.Event);
			_dictionary[item.Event].Add(item);
			item.PropertyChanged += Split_PropertyChanged;
		}

		public bool Remove(AutoSplit item)
		{
			if (!_dictionary.ContainsKey(item.Event) || !_dictionary[item.Event].Remove(item))
				return false;
			_list.Remove(item);
			item.PropertyChanged -= Split_PropertyChanged;
			return true;
		}

		public bool Remove(string name)
		{
			foreach (var split in this)
			{
				if (split.Name == name)
				{
					Remove(split);
					return true;
				}
			}
			return false;
		}

		public void RemoveAt(int index) => Remove(_list[index]);

		public void Clear()
		{
			foreach (var split in this)
				split.PropertyChanged -= Split_PropertyChanged;
			_list.Clear();
			_dictionary.Clear();
		}

		public bool Contains(AutoSplit item) => _dictionary.ContainsKey(item.Event) && _dictionary[item.Event].Contains(item);

		public int IndexOf(AutoSplit item) => _list.IndexOf(item);

		public void CopyTo(AutoSplit[] array, int arrayIndex) => _list.CopyTo(array, arrayIndex);

		public IEnumerator<AutoSplit> GetEnumerator() => _list.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => _list.GetEnumerator();

	}
}
