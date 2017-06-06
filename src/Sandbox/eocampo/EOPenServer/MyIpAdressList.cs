using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace EOPenServer
{
    public class IPAddressList : IEnumerable //Implement ICollection
    {
        private IPAddress[] addressArray;        

        private IPAddressList() {

        }

        public IPAddressList(IPAddress[] addressArray) {
            this.addressArray = addressArray;
        }

        IEnumerator IEnumerable.GetEnumerator() {
            IPAddressEnumerator enumerator = new IPAddressEnumerator(this);
            return enumerator;
        }

        public class IPAddressEnumerator : IEnumerator
        {
            private int position = -1;
            private IPAddressList addressList;

            private IPAddressEnumerator() {
            }

            public IPAddressEnumerator(IPAddressList addressList) {
                this.addressList = addressList;
            }

            public object Current {
                get { return this.addressList.addressArray[this.position]; }
            }           

            public bool MoveNext() {
                if (this.position < this.addressList.addressArray.Length - 1) {
                    this.position = this.position + 1;
                    return true;
                }
                return false;
            }

            public void Reset() {
                this.position = -1;
            }
        }
    }

    public class IPAddressFilteredEnumerable : IEnumerable
    {
        private IPAddress[] addressArray;
        private AddressFamily family;

        private IPAddressFilteredEnumerable() {

        }

        public IPAddressFilteredEnumerable(IPAddress[] addressArray, AddressFamily family) {
            this.addressArray = addressArray;
            this.family = family;
        }

        IEnumerator IEnumerable.GetEnumerator() {
            IPAddressEnumerator enumerator = new IPAddressEnumerator(this);
            return enumerator;
        }

        public class IPAddressEnumerator : IEnumerator
        {
            private int position = -1;
            private IPAddressFilteredEnumerable addressList;

            private IPAddressEnumerator() {
            }

            public IPAddressEnumerator(IPAddressFilteredEnumerable addressList) {
                this.addressList = addressList;
            }

            public object Current {
                get { return this.addressList.addressArray[this.position]; }
            }

            public bool MoveNext() {
                while (this.position < this.addressList.addressArray.Length - 1) {
                    this.position = this.position + 1;

                    if(this.addressList.addressArray[this.position].AddressFamily == this.addressList.family)
                    return true;
                }
                return false;
            }

            public void Reset() {
                this.position = -1;
            }
        }
    }

    public class IPAddressFilteredList : IEnumerable, ICollection, IList
    {
        private IPAddress[] addressArray;
        private AddressFamily family;

        private IPAddressFilteredList() {

        }

        public IPAddressFilteredList(IPAddress[] addressArray, AddressFamily family) {
            this.addressArray = addressArray;
            this.family = family;
        }

        IEnumerator IEnumerable.GetEnumerator() {
            IPAddressEnumerator enumerator = new IPAddressEnumerator(this);
            return enumerator;
        }

        public class IPAddressEnumerator : IEnumerator
        {
            private int position = -1;
            private IPAddressFilteredList addressList;

            private IPAddressEnumerator() {
            }

            public IPAddressEnumerator(IPAddressFilteredList addressList) {
                this.addressList = addressList;
            }

            public object Current {
                get { return this.addressList.addressArray[this.position]; }
            }

            public bool MoveNext() {
                while (this.position < this.addressList.addressArray.Length - 1) {
                    this.position = this.position + 1;

                    if (this.addressList.addressArray[this.position].AddressFamily == this.addressList.family)
                        return true;
                }
                return false;
            }

            public void Reset() {
                this.position = -1;
            }
        }

        public void CopyTo(Array array, int index) {
            throw new NotImplementedException();
        }

        public int Count {
            get {
                int pos = 0;
                int count = 0;
                while (pos < this.addressArray.Length) {                    
                    if (this.addressArray[pos++].AddressFamily == this.family)
                        count++;                    
                }
                return count;
            }
        }

        public bool IsSynchronized {
            get { return false; }
        }

        private object syncRoot = new object();

        public object SyncRoot {
            get { return this.syncRoot; }
        }

        #region IList Implementation

        public int Add(object value) {
            throw new NotImplementedException();
        }

        public void Clear() {
            throw new NotImplementedException();
        }

        public bool Contains(object value) {
            throw new NotImplementedException();
        }

        public int IndexOf(object value) {
            throw new NotImplementedException();
        }

        public void Insert(int index, object value) {
            throw new NotImplementedException();
        }

        public bool IsFixedSize {
            get { throw new NotImplementedException(); }
        }

        public bool IsReadOnly {
            get { throw new NotImplementedException(); }
        }

        public void Remove(object value) {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index) {
            throw new NotImplementedException();
        }

        public object this[int index] {
            get {
                throw new NotImplementedException();
            }
            set {
                throw new NotImplementedException();
            }
        }

        #endregion //IList
    }    


#region Notas acerca del this

    public class Datos
    {
        public int x;
        public int y;
        public int z;
    }

    public class Comportamiento
    {
        public void CalculaZ(Datos datos) {
            datos.z = datos.x * datos.y; 
        }
    }

    public class ObjetodePOO // POO: Programación Orientada a Objetos
    {
        public int x;
        public int y;
        public int z;

        //public void CalculaZ(ObjetodePOO datos) {
        //    datos.z = datos.x * datos.y;
        //}

        //public int Z {
        //    get {
        //        return this.z;
        //    }
        //    set {
        //        this.z = value;
        //    }
        //}
        //public int __get_Z(ObjetodePOO this) {
        //    return this.z;
        //}
        //public void __set_Z(ObjetodePOO this, int value) {
        //    this.z = value;
        //}


        //public void CalculaZ(ObjetodePOO this) {
        //    this.z = this.x * this.y;
        //}

        public void CalculaZ() {
            this.z = this.x * this.y;
        }
    }

    public class Programa
    {
        public Programa() {

        }

        public void Run() {
            //Datos datos = new Datos();
            //Comportamiento comportamiento = new Comportamiento();
            //datos.x = 10;
            //datos.y = 5;            
            //comportamiento.CalculaZ(datos);
            //System.Console.WriteLine("z: " + datos.z.ToString());

            ObjetodePOO objeto = new ObjetodePOO();
            objeto.x = 10;
            objeto.y = 5;
            objeto.CalculaZ(); // compilador, cuando ve eso, en realidad el escribe: objeto.CalculaZ(objeto);  // CalculaZ(objeto);
            System.Console.WriteLine("z: " + objeto.z.ToString());

            //Phone phone = new Phone();
            //phone.Number = "3132201";
            //phone.Call();

            //PhoneNumber number = malloc();
            //SetPhoneNumber(number, "3132201");
            //Phone_Call(number);

            

            //Phone_
            //ADdress_
            //Contact_


        }
    }

#endregion
}
