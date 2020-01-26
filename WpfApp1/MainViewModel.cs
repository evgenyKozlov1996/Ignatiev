using com.calitha.goldparser;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Scanner;
using System.CodeDom.Compiler;

namespace WpfApp1
{
    public class MainViewModel : INotifyPropertyChanged
    {
		private string fileName = "test.txt";
        private List<Node> root;
        private Scaner scaner;

        public List<Node> MyItemsSource
        {
            get { return root; }
            set { root = value; OnPropertyChanged(); }
        }

        private string polishtext;

        public string PolishText
        {
            get { return polishtext; }
            set { polishtext = value; OnPropertyChanged(); }
        }

        private string code;
        public string Code
        {
            get { return code; }
            set { code = value; OnPropertyChanged(); }
        }

        private string state;

        public string State
        {
            get { return state; }
            set { state = value; OnPropertyChanged(); }
        }

        private string scannertext;

        public string ScannerText
        {
            get { return scannertext; }
            set { scannertext = value; OnPropertyChanged(); }
        }

        private RelayCommand startCommand;

        private RelayCommand openfileCommand;

        private RelayCommand startScanner;

        private RelayCommand polishconvertCommand;

        public RelayCommand PolishConvertCommand
        {
            get
            {

                return polishconvertCommand ??
                (polishconvertCommand = new RelayCommand(obj =>
                {
                    root[0] = TreeConverter.ConvertGrammarTreeToOperationTree(root[0]);
                    OnPropertyChanged(nameof(MyItemsSource));
                    /*try {
                        StartCommand.Execute(new object());

                        if (root[0] != null)
                        {
                            var compiler = CodeDomProvider.CreateProvider("CSharp");
                            var parameters = new CompilerParameters
                            {
                                CompilerOptions = "/t:library",
                                GenerateInMemory = true,
                                IncludeDebugInformation = false
                            };
                            String templatestart = @"
                    using System;
                        namespace Test
                        {
                               public class TestClass
                               {
                                     public string TestMethod()
                                     {
                                        string result = string.Empty;
                                    ";
                            string text = Code.Substring(Code.IndexOf('{') + 1);
                            text = text.Remove(text.LastIndexOf('}'));
                            int outputIndex = text.IndexOf("output") + 6;
                            while (outputIndex != 5)
                            {
                                int spaceCount = 0;
                                int currIndex = outputIndex;
                                while (text[currIndex].Equals(' '))
                                {
                                    spaceCount++;
                                    currIndex++;
                                }
                                text = text.Remove(outputIndex, spaceCount);
                                int index = outputIndex;
                                while (!text[index].Equals(';'))
                                {
                                    if (text[index].Equals('(') || text[index].Equals(')') || text[index].Equals(' '))
                                    {
                                        text = text.Remove(index, 1);
                                    }
                                    else if (text[index].Equals(','))
                                    {
                                        text = text.Remove(index, 1);
                                        text = text.Insert(index, " + \"\\n\" + ");
                                        index += 9;
                                    }
                                    else
                                    {
                                        index++;
                                    }
                                }
                                text = text.Remove(outputIndex-6, 6);
                                text = text.Insert(outputIndex - 6, "result += ");

                                outputIndex = text.IndexOf("output") + 6;
                            }

                            String templateend = @"return result;}             
                                }
                         }";

                            string resultcode = templatestart + text + templateend;
                            CompilerResults results = compiler.CompileAssemblyFromSource(parameters, resultcode);

                            var instance = results.CompiledAssembly.CreateInstance("Test.TestClass");

                            string resultMsg = results.CompiledAssembly.GetType("Test.TestClass").GetMethod("TestMethod").Invoke(instance, new string[] { }).ToString();

                            State = resultMsg;
                        }
                        //preOrder(root[0]);
                    }
                    catch (Exception ex)
                    {

                    }*/
                }));
            }
        }

        public RelayCommand StartScanner
        {
            get
            {

                return startScanner ??
                (startScanner = new RelayCommand(obj =>
                {
                    root = new List<Node>();
                    root.Add(new Node());
                    OnPropertyChanged("MyItemsSource");

                    State = "";

                    scaner = new Scaner(Code);

                    try
                    {
                        scaner.ScanText();
                    }
                    catch (ParseErrorException ex)
                    {
                        State = ex.Message;
                    }
                    catch (IndexOutOfRangeException)
                    {
                        // Do nothing, it's just a signal that the text ended.
                    }
                    ScannerText = "";
                    foreach (Scanner.Token t in this.scaner.ResultTokens)
                    {
                        ScannerText += t.Value + " - " + t.TokenType.ToString("G");
                        ScannerText += Environment.NewLine;
                    }
                }));
            }
        }
        public RelayCommand OpenFileCommand
        {
            get {

                return openfileCommand ??
                (openfileCommand = new RelayCommand(obj =>
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
                    openFileDialog.ShowDialog();
                    if (openFileDialog.FileName.Length == 0)
                        return;
                    string filename = openFileDialog.FileName;
                    using (TextReader reader = File.OpenText(filename))
                    {
                        Code = reader.ReadToEnd();                        
                    }
                }));
            }
        }
        public RelayCommand StartCommand
        {
            get
            {
                return startCommand ??
                  (startCommand = new RelayCommand(obj =>
                  {
                      StartScanner.Execute(new object());
                      if (ScannerText!=null && !ScannerText.Equals(string.Empty))
                      {
                          root = new List<Node>();
                          root.Add(new Node());
                          State = "";
                          MyParser parser = new MyParser("Cshort8.cgt", this);
                          var a = parser.Parse(Code);

                          if (a != null)
						  {
							  ConvertToTree(a, root[0]);
							  root[0] = TreePreparer.PrepareTree(root[0]);
						  }
                              

                          OnPropertyChanged("MyItemsSource");
                      }
                  }));
            }
        }

        public MainViewModel()
        {
            root = new List<Node>();
            root.Add(new Node());

			using (TextReader reader = File.OpenText(fileName))
			{
				Code = reader.ReadToEnd();
			}
			//    String test = @"START {
			//    int a = 0;
			//    if(a == 0)
			//    a = 2;
			//        for(int i = 0; i < 10; i++)
			//        {
			//            a = a + i;
			//        }
			//}";
			//    MyParser parser = new MyParser("Cshort2.cgt");
			//    var a = parser.Parse(test);
			//    //DrawTree(a, depth);
			//    ConvertToTree(a, root[0]);
			//    OnPropertyChanged("MyItemsSource");
		}

        private void ConvertToTree(com.calitha.goldparser.Token token, Node currentNode1)
        {
            if (token is TerminalToken)
            {
                currentNode1.Data = (token as TerminalToken).Text;
            }
            else
            {
                currentNode1.Data = (token as NonterminalToken).Symbol.Name;
                for (int i = 0; i < (token as NonterminalToken).Tokens.Length; i++)
                {
                    Node temp = currentNode1;
					Node childNode = new Node()
					{
						Parent = currentNode1
					};
                    currentNode1.Children.Add(childNode);
                    currentNode1 = currentNode1.Children.Last();
                    ConvertToTree((token as NonterminalToken).Tokens[i], currentNode1);
                    currentNode1 = temp;
                }
            }
        }

        private void postOrder(List<Node> node)
        {
            if (node[0] == null) return;
            for(int i = 0; i < node[0].Children.Count; i++)
            {
                postOrder(node[0].Children[i]);
            }
            PolishText += node[0].Data + " ";
        }

        private void postOrder(Node node)
        {
            if (node == null) return;
            for (int i = 0; i < node.Children.Count; i++)
            {
                postOrder(node.Children[i]);
            }
            if(node.Children.Count == 0)
            PolishText += node.Data + " ";
        }

        private void preOrder(Node node)
        {
            if (node == null) return;
            if (node.Children.Count == 0)
                PolishText += node.Data + " ";
            for (int i = 0; i < node.Children.Count; i++)
            {
                preOrder(node.Children[i]);
            }
            
                
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
