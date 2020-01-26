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

namespace WpfApp1
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private List<Node> root;
        private Scaner scaner;

        public List<Node> MyItemsSource
        {
            get { return root; }
            set { root = value; OnPropertyChanged(); }
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
                    try
                    {
                        StartCommand.Execute(new object());

                        State = "";
                        result = "";
                        //root[0] = TreeConverter.ConvertGrammarTreeToOperationTree(root[0]);
                        //OnPropertyChanged(nameof(MyItemsSource));

                        // Script
                        string script = Code;
                        script = "main();" + script;

                        Jint.Engine eng = new Jint.Engine().SetValue("consolelog", new Action<object>(Log));
                        eng.Execute(script);

                        State = result;
                    }
                    catch(Exception ex)
                    {

                    }
                }));
            }
        }

        string result = "";
        object obj = new object();

        private void Log(object str)
        {
            lock (obj) {
                result += str + "\n";
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
                          MyParser parser = new MyParser("JavaScript.cgt", this);
                          var a = parser.Parse(Code);

                          if (a != null)
						  {
							  ConvertToTree(a, root[0]);
							  //root[0] = TreePreparer.PrepareTree(root[0]);
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
            //PolishText += node[0].Data + " ";
        }

        private void postOrder(Node node)
        {
            if (node == null) return;
            for (int i = 0; i < node.Children.Count; i++)
            {
                postOrder(node.Children[i]);
            }
            if (node.Children.Count == 0) { }
            //PolishText += node.Data + " ";
        }

        private void preOrder(Node node)
        {
            if (node == null) return;
            if (node.Children.Count == 0)
                //PolishText += node.Data + " ";
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
