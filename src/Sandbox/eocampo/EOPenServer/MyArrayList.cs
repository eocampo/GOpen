using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EOPenServer
{   
    public class LettersList : IEnumerable // : IList<IPAddress>, ICollection<IPAddress>, IEnumerable<IPAddress>, IEnumerable
    {
        private string theString;

        public LettersList(string theString) {
            this.theString = theString;
        }

        //public string TheString {
        //    get { return this.theString; }
        //}

        IEnumerator IEnumerable.GetEnumerator() {
            StringEnumerator enumerator = new StringEnumerator(this);
            return enumerator;
        }

        public class StringEnumerator : IEnumerator
        {
            private int pos = -1;
            private LettersList parentClass;

            private StringEnumerator() {
            }

            public StringEnumerator(LettersList parentClass) {
                this.parentClass = parentClass;
            }

            public object Current {
                get { return this.parentClass.theString.Substring(pos, 1); }
            }

            public bool MoveNext() {
                if (this.pos < this.parentClass.theString.Length - 1) {
                    this.pos = this.pos + 1;
                    return true;
                }
                return false;
            }

            public void Reset() {
                this.pos = -1;
            }
        }        
    }

    public class VocalsList : IEnumerable // : IList<IPAddress>, ICollection<IPAddress>, IEnumerable<IPAddress>, IEnumerable
    {
        private string theString;

        public VocalsList(string theString) {
            this.theString = theString;
        }        

        IEnumerator IEnumerable.GetEnumerator() {
            StringEnumerator enumerator = new StringEnumerator(this);
            return enumerator;
        }

        public class StringEnumerator : IEnumerator
        {
            private int pos = -1;
            private VocalsList parentClass;

            private StringEnumerator() {
            }

            public StringEnumerator(VocalsList parentClass) {
                this.parentClass = parentClass;
            }

            public object Current {
                get { return this.parentClass.theString.Substring(pos, 1); }
            }

            public bool MoveNext() {
                while (this.pos < this.parentClass.theString.Length - 1) {
                    this.pos = this.pos + 1;
                    if (this.parentClass.theString.Substring(this.pos, 1).Contains("a")
                        || this.parentClass.theString.Substring(this.pos, 1).Contains("e")
                        || this.parentClass.theString.Substring(this.pos, 1).Contains("i")
                        || this.parentClass.theString.Substring(this.pos, 1).Contains("o")
                        || this.parentClass.theString.Substring(this.pos, 1).Contains("u"))
                        return true;
                }
                return false; 
            }

            public void Reset() {
                this.pos = -1;
            }
        }


    }

    public class CustomFixedEnumerable : IEnumerable // : IList<IPAddress>, ICollection<IPAddress>, IEnumerable<IPAddress>, IEnumerable
    {
        IEnumerator IEnumerable.GetEnumerator() {
            MyEnumerator my = new MyEnumerator();
            return my;
        }

        public class MyEnumerator : IEnumerator
        {
            private int pos = -1;

            public object Current {
                get {
                    switch (pos) {
                        case -1: throw new Exception("Debes llamar primero a Next()");
                            break;
                        case 0: return "a";
                        case 1: return "b";
                        case 2: return "c";
                    }
                    throw new Exception("Estamos en una posición no válida");
                }
            }

            public bool MoveNext() {
                if (this.pos < 2) {
                    this.pos = this.pos + 1;
                    return true;
                }
                return false;
            }

            public void Reset() {
                this.pos = -1;
            }
        }
    }

    public class MainTest
    {
        public MainTest() {            
        }

        public static void ManualEnumerate(IEnumerable list) {
            IEnumerator enumerator = list.GetEnumerator();
            //while (enumerator.MoveNext()) {
            //    System.Console.WriteLine(enumerator.Current.ToString());
            //}

            enumerator.MoveNext(); // se acomoda en la a
            System.Console.WriteLine(enumerator.Current.ToString()); // imprime a

            enumerator.MoveNext(); // se acomoda en la b
            System.Console.WriteLine(enumerator.Current.ToString()); // imprime b

            enumerator.Reset();
            enumerator.MoveNext(); // se acomoda en la a
            System.Console.WriteLine(enumerator.Current.ToString()); // imprime a
        }

        public static void WhileEnumerate(IEnumerable list) {
            IEnumerator enumerator = list.GetEnumerator();
            while (enumerator.MoveNext()) {
                System.Console.WriteLine(enumerator.Current.ToString());
            }            
        }

        public static void ForEachEnumerate(IEnumerable list) {
            foreach(object obj in list) {
                System.Console.WriteLine(obj.ToString());
            }
        }

        public static void RunTest1() {
            CustomFixedEnumerable list = new CustomFixedEnumerable();
            ManualEnumerate(list);
        }

        public static void RunTest2() {
            CustomFixedEnumerable list = new CustomFixedEnumerable();
            WhileEnumerate(list);
        }

        public static void RunTest3() {
            CustomFixedEnumerable list = new CustomFixedEnumerable();
            ForEachEnumerate(list);
        }

        public static void RunTest4() {
            LettersList list = new LettersList("Hola Mundo!");            
            ForEachEnumerate(list);
        }

        public static void RunTest5() {
            VocalsList list = new VocalsList("Hola Mundo!");
            ForEachEnumerate(list);
        }


        public static void Run() {
            //RunTest1();
            //RunTest2();
            //RunTest3();
            //RunTest4();
            RunTest5();

            //ArrayList list = new ArrayList();
            //list.Add("a");
            //list.Add("b");
            //list.Add("c");
            //string[] lista = new string[] { "a", "b", "c" };
            //MainTest test = new MainTest();
            //IPAddressList list = new IPAddressList();
            //IEnumerable enumerable = list;
            //Enumerate(list);
            // Enumerate(lista);            
        }
    }
}
