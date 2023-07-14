using System;
using System.Collections;
using System.Collections.Generic;

namespace hashes
{
	public class ReadonlyBytes: IEnumerable<byte>
	{
		byte[] data;
		int CodeHash;
        

        public ReadonlyBytes(params byte[] bytes) 
		{
			if (bytes == null) throw new System.ArgumentNullException();
			else
			{
				data = bytes;
				CodeHash=this.GetHashCode();
			}
		}

		public bool Equals(ReadonlyBytes another)
		{
			if(another==null || another.GetType() != this.GetType()) return false;
            if (another.data.Length != data.Length) return false;
            if (another.CodeHash != this.CodeHash) return false;				
			for(int i=0;i<this.Length; i++)
			{
				if (data[i] != this[i]) return false;
			}
			
			return true;
		}

		public override int GetHashCode() 
		{
			unchecked
			{
				uint hash = 0;
				const uint fnvprime = 0x811C9DC5;

				foreach (byte b in data)
				{
					hash ^= b;
					hash *= fnvprime;
				}

				return (int)hash;
			}
        }

        public IEnumerator<byte> GetEnumerator()
        {
            int i = 0;
            while (i < data.Length)
            {

                yield return data[i];
                i++;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
			return GetEnumerator();
        }

        public byte this[int index] 
		{ 
			get 
			{ 
				if(index<0 || index>data.Length) throw new IndexOutOfRangeException();
				else return data[index]; 
			}
			
		}

		public int Length
		{
			get { return data.Length; }
		}

		public override string ToString()
		{
			//if(data.Length == 0) return string.Empty;
			return "["+string.Join(", ",data)+"]";
		}
	}
}