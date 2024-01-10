using System;
using System.Windows.Forms;

namespace Course_Connect
{
    public partial class ConvertToExcel : Form
    {
        public ConvertToExcel()
        {
            InitializeComponent();
        }

        public static void Convert(DataGridView thisDGV, string fileName, string sheetName)
        {
            // Khai báo các thư viện cần thiết:
            Microsoft.Office.Interop.Excel.Application Excel;
            Microsoft.Office.Interop.Excel.Workbook Workbook;
            Microsoft.Office.Interop.Excel.Worksheet Worksheet;

            // Tạo đối tượng COM:
            Excel = new Microsoft.Office.Interop.Excel.Application();
            Excel.Visible = false;
            Excel.DisplayAlerts = false;

            // Tạo mới một Workbook & Worksheet bằng phương thức Add():
            Workbook = Excel.Workbooks.Add(Type.Missing);
            Worksheet = (Microsoft.Office.Interop.Excel.Worksheet)Workbook.Sheets["Sheet1"];

            // Đặt tên cho Sheet:
            Worksheet.Name = sheetName;
            
            // Export header trong DataGridView:
            for (int i = 1; i < thisDGV.ColumnCount; i++)
                Worksheet.Cells[1, i] = thisDGV.Columns[i].HeaderText;

            // Export nội dung trong DataGridView:
            for (int i = 0; i < thisDGV.RowCount; i++)
                for (int j = 1; j < thisDGV.ColumnCount; j++)
                {
                    object cellValue = thisDGV.Rows[i].Cells[j].Value;
                    string valueAsString = cellValue?.ToString() ?? string.Empty;
                    Worksheet.Cells[i + 2, j] = valueAsString;
                }
            /* Giải thích:
                + object cellValue = thisDGV.Rows[i].Cells[j].Value;: Dòng này truy cập vào ô tại hàng i và 
                  cột j trong DataGridView (thisDGV) và gán giá trị của ô đó vào biến cellValue. 
                  Đối tượng Value của ô chứa dữ liệu thực tế của ô đó.
                + string valueAsString = cellValue?.ToString() ?? string.Empty;: Dòng này chuyển đổi giá trị 
                  của cellValue thành một chuỗi (string). Toán tử '?.' được sử dụng để kiểm tra xem cellValue có 
                  null không trước khi gọi phương thức ToString(). Nếu cellValue là null, biến valueAsString sẽ 
                  được gán giá trị mặc định là chuỗi rỗng (string.Empty). Nếu cellValue không null, phương thức 
                  ToString() sẽ được gọi để chuyển đổi giá trị thành chuỗi.
                + Worksheet.Cells[i + 2, j] = valueAsString;: Dòng này gán giá trị của valueAsString vào ô tại 
                  hàng i + 2 và cột j trong đối tượng Worksheet. Đoạn mã này giả định rằng Worksheet là một đối 
                  tượng đại diện cho một bảng tính (Excel) và Cells là một thuộc tính hoặc phương thức để 
                  truy cập vào ô trong bảng tính đó.
            */

            // Mở cửa số SaveFileDialog để chọn nơi lưu file:
            SaveFileDialog saveWindow = new SaveFileDialog();

            // Thiết lập các thuộc tính của SaveFileDialog:
            saveWindow.Title = "Chọn vị trí lưu file";
            saveWindow.Filter = "File Excel|*.xlsx";
            saveWindow.FileName = fileName + ".xlsx";

            // Nếu bấm "OK" - đã xác nhận nơi lưu
            if (saveWindow.ShowDialog() == DialogResult.OK)
            {
                // Lưu file vào đường dẫn hiện hành:
                string filePath = saveWindow.FileName;
                Workbook.SaveAs(filePath);

                // Đóng workbook và kết thúc việc xuất file:
                Workbook.Close();
                Excel.Quit();
                MessageBox.Show("Xuất dữ liệu ra Excel thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
