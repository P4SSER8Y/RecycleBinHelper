using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Shell32;
using System.Windows.Forms;

namespace RecycleBinHelper
{
    class Program
    {
        [Flags]
        private enum Arguments
        {
            Silent = 0x0001,
        }

        private static void UnknownOptionAlert(string item)
        {
            MessageBox.Show($"Unknow option: {item}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        [STAThread] // STAThread must be set to use Shell32.Shell()
        static void Main(string[] args)
        {
            Arguments arguments = 0x00;
            uint daysAgo = UInt32.MaxValue;

            foreach (string item in args)
            {
                if (item[0] == '-')
                {
                    if (item.Length < 2)
                    {
                        UnknownOptionAlert(item);
                        return;
                    }
                    switch (item[1])
                    {
                        case '-':
                            if (item.Length < 4)
                            {
                                UnknownOptionAlert(item);
                                return;
                            }
                            if (item.ToLower() == "--silent")
                                arguments |= Arguments.Silent;
                            break;
                        case 's':
                            if (item.Length > 2)
                            {
                                UnknownOptionAlert(item);
                                return;
                            }
                            arguments |= Arguments.Silent;
                            break;
                        default:
                            UnknownOptionAlert(item);
                            return;
                    }
                }
                else if (!(uint.TryParse(item, out daysAgo)))
                {
                    UnknownOptionAlert(item);
                    return;
                }
            }

            if (daysAgo == uint.MaxValue)
            {
                MessageBox.Show("Argument Missed: Days", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var deletedFiles = new DeletedFiles();

            if ((args.Length == 0) || (!(uint.TryParse(args[0], out daysAgo))))
                daysAgo = 0;

            var dateBefore = DateTime.Today.AddDays(-daysAgo);
            Console.WriteLine("Select the files deleted before {0}", dateBefore.Date.AddDays(1).AddSeconds(-1));

            deletedFiles.SelectDaysBefore(daysAgo);
            
            if (arguments.HasFlag(Arguments.Silent))
            {
                deletedFiles.Empty();
            }
            else
            {
                if (deletedFiles.Items.Count == 0)
                {
                    MessageBox.Show("No file", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var ret =
                    MessageBox.Show($"{deletedFiles.Items.Count} files, {deletedFiles.TotalFileSize} bytes\nDeleted?",
                        "Confirm?",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Question);

                if (ret == DialogResult.OK)
                {
                    var message = deletedFiles.Empty();
                    if (message.Length > 0)
                        MessageBox.Show(message+"\nYou may need to delete them yourself.", "ERROR Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}