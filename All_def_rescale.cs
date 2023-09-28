using System.Text.RegularExpressions;

namespace All_def_rescale
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("arg1 = filepath, arg2 = float scale factor");
            Console.WriteLine("Example: C:\\All_def.txt 1,33");
            if (args.Length != 2)
            {
                Console.WriteLine("Missing cmd line param");
                Console.ReadKey();
                return;
            }

            string path = args[0];
            float scalefactor = float.Parse(args[1]);

            List<string> Scaled = new List<string>();
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        line = line.TrimStart();
                        if (!line.StartsWith("#"))
                        {
                            line = line.Replace("\t", " ");
                        }

                        if (line.StartsWith("source"))
                        {
                            string noTab = Regex.Replace(line, @"\s+", " ");
                            string[] tmp  = noTab.Split(" ");
                            for (int i = 3; i < tmp.Length-1; i++)
                            {
                                int scale = (int)Math.Round(float.Parse(tmp[i]) * scalefactor);
                                tmp[i] = scale.ToString();
                                Console.WriteLine(tmp[i]);
                            }
                            line = $"\t{tmp[0],-5}\t{tmp[1],-40}\t{tmp[2],-10}\t{tmp[3],-5}\t{tmp[4],-10}\t{tmp[5],-5}\t{tmp[6],-10}\t{tmp[7],-5}\t{tmp[8],0}";
                            Scaled.Add(line);

                            Console.WriteLine(line);
                        }
                        else
                        {
                            Scaled.Add(line);
                            Console.WriteLine(line);
                        }
                    }
                }
                File.WriteAllLinesAsync(Path.Combine(Environment.CurrentDirectory, "All_def_scaled.txt"), Scaled);
                Console.WriteLine("Done");
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
            Console.ReadKey();
        }
    }
}