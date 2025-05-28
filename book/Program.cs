using System;
using System.IO; // Thêm để sử dụng Path và File
using System.Windows.Forms;
using System.Reflection; // Có thể cần cho một số thông tin assembly, hoặc để lấy AppContext.BaseDirectory nếu bạn không dùng .NET Core 3.0+
using System.Threading; // Thêm để sử dụng ThreadExceptionEventHandler

namespace book // Giữ nguyên namespace của bạn
{
    internal static class Program
    {
        // Đặt tên và đường dẫn file log. File log sẽ được tạo trong cùng thư mục với file .exe
        // AppContext.BaseDirectory là cách tốt để lấy thư mục chứa ứng dụng,
        // đặc biệt quan trọng với single-file executable.
        private static string logFilePath = Path.Combine(AppContext.BaseDirectory, "app_runtime_log.txt");

        [STAThread]
        private static void Main(string[] args) // Thêm string[] args nếu bạn cần xử lý tham số dòng lệnh, nếu không có thể bỏ qua
        {
            try
            {
                // Xóa log cũ nếu có (tùy chọn, để dễ theo dõi lần chạy mới nhất)
                if (File.Exists(logFilePath))
                {
                    File.Delete(logFilePath);
                }

                Log("Application starting...");
                // Log thêm thông tin môi trường có thể hữu ích
                Log($"Operating System: {Environment.OSVersion.VersionString}");
                Log($"Is 64bit OS: {Environment.Is64BitOperatingSystem}");
                Log($".NET Version: {Environment.Version}"); // Hoặc System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription cho .NET Core/.NET 5+
                Log($"Current Directory: {Environment.CurrentDirectory}");
                Log($"Base Directory (for log file): {AppContext.BaseDirectory}");
                Log($"User: {Environment.UserName}");

                // Đăng ký các trình xử lý ngoại lệ toàn cục
                // Điều này RẤT QUAN TRỌNG để bắt các lỗi không được xử lý trong ứng dụng
                Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

                Log("Global exception handlers registered.");

                Application.EnableVisualStyles();
                Log("Visual styles enabled.");

                Application.SetCompatibleTextRenderingDefault(false);
                Log("Compatible text rendering set to false.");

                Log("Attempting to run main form 'begin'...");
                Application.Run(new begin()); // Form chính của bạn
                Log("Application exited normally after Application.Run(new begin()).");
            }
            catch (Exception ex)
            {
                // Khối catch này sẽ bắt các ngoại lệ xảy ra trực tiếp trong hàm Main,
                // trước khi Application.Run() được gọi hoặc nếu Application.Run() không thể bắt đầu.
                Log($"CRITICAL ERROR IN MAIN METHOD: {ex.ToString()}");
                // Hiển thị thông báo lỗi cho người dùng nếu có thể
                MessageBox.Show($"A critical error occurred during application startup:\n\n{ex.Message}\n\nDetails:\n{ex.ToString()}",
                                "Application Startup Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Hàm ghi log
        public static void Log(string message)
        {
            try
            {
                // Ghi kèm timestamp cho mỗi dòng log
                File.AppendAllText(logFilePath, $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] {message}{Environment.NewLine}");
            }
            catch (Exception ex)
            {
                // Nếu việc ghi log thất bại (ví dụ không có quyền ghi), chúng ta không muốn ứng dụng bị crash chỉ vì log.
                // Bạn có thể xem xét cách xử lý tinh tế hơn ở đây nếu cần, ví dụ ghi ra Console.
                Console.WriteLine($"Failed to write to log: {ex.Message}");
            }
        }

        // Trình xử lý ngoại lệ cho các luồng UI
        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            string errorMessage = $"UNHANDLED UI THREAD EXCEPTION:\n{e.Exception.ToString()}";
            Log(errorMessage);
            MessageBox.Show(errorMessage, "Unhandled UI Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            // Bạn có thể quyết định đóng ứng dụng ở đây nếu cần
            // Environment.Exit(1);
        }

        // Trình xử lý ngoại lệ cho các luồng không phải UI (background threads)
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            string errorMessage = $"UNHANDLED NON-UI THREAD EXCEPTION (IsTerminating: {e.IsTerminating}):\n{ex?.ToString() ?? "N/A"}";
            Log(errorMessage);
            // Không nên hiển thị MessageBox từ đây nếu IsTerminating là true hoặc nếu đây là luồng background
            // vì ứng dụng có thể đang trong trạng thái không ổn định. Chỉ ghi log là an toàn nhất.
            // Nếu bạn muốn hiển thị lỗi trước khi ứng dụng chắc chắn thoát:
            if (ex != null) // Kiểm tra để đảm bảo ex không null
            {
                MessageBox.Show(errorMessage, "Unhandled Non-UI Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // Ứng dụng thường sẽ tự thoát sau một unhandled exception trên non-UI thread.
        }
    }
}