using Renci.SshNet;

namespace ScpLibrary
{
    public class ScpHelper
    {
        private string host;
        private string username;
        private string password;

        public ScpHelper(string host, string username, string password)
        {
            this.host = host;
            this.username = username;
            this.password = password;
        }

        public bool IsExist(string path)
        {
            try
            {
                using (var client = new SshClient(host, username, password))
                {
                    client.Connect();
                    var command = client.RunCommand("ls " + path);
                    client.Disconnect();

                    return string.IsNullOrEmpty(command.Error);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }

        public void CreateDirectory(string path)
        {
            try
            {
                using (var client = new SshClient(host, username, password))
                {
                    client.Connect();
                    client.RunCommand("mkdir -p " + path);
                    client.Disconnect();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public void CreateFile(string path)
        {
            try
            {
                using (var client = new SshClient(host, username, password))
                {
                    client.Connect();
                    client.RunCommand("touch " + path);
                    client.Disconnect();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public void CopyFile(string sourceFilePath, string destinationFilePath)
        {
            try
            {
                using (var client = new SshClient(host, username, password))
                {
                    client.Connect();

                    using (var scp = new ScpClient(host, username, password))
                    {
                        scp.Connect();
                        scp.Upload(new System.IO.FileInfo(sourceFilePath), destinationFilePath);
                    }

                    client.Disconnect();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
