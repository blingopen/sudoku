# MySudoku
homework of software engineering 2018

文件夹Documents为个人上传的分析与设计文档以及测试用例，比博客中要稍微详细。  

文件夹MySudoku为主要程序,sudoku.exe只能在命令行下运行,其命令格式为：  
生成数独终局： /*文件位置*/sudoku.exe -c *有效数字*  
 求解数独： /*文件位置*/sudoku.exe -s *绝对路径/文件名*  
其中文件位置为可执行文件所在路径，有效数字为[1，1000000]，其余数字均会提示错误信息  
绝对路径为数独谜题的存放路径，文件仅支持.txt文件  
文件夹内部主要大致为：(只列举重要文件）  
/MySudoku  
-/MySudoku.sln（解决方案）  
-/MySudoku  
--/Sudoku.cs（Sudoku类）  
--/Program.cs（主函数）  
--/MySudoku.csproj（工程文件）  
--/BIN  
---/sudoku.exe（主要应用程序）  
---/sudoku.txt（生成文本）

文件夹SudokuUI为简易的带有UI界面的数独游戏软件，双击Sudoku.exe文件即可执行。  
其中，点击“开始游戏”即可开始游戏，点击“重新来过”即可回到初始界面，点击“开始游戏”即可重新游戏。    
数独仅支持键盘输入。  
当完成全部9×9方格后，系统会自动判断是否正确，若正确会恭喜完成，若错误会提示重新检查。  
文件夹内部主要大致为：  
/SudokuUI  
-/SudokuUI.sln（解决方案）  
-/SudokuUI  
--/Form1.cs （主要窗体代码）  
--/Form1.resx  
--/Program.cs （主程序代码）  
--/Sudoku.cs （Sudoku类）  
--/SudokuUI.csproj （工程文件）  
--/UIBIN  
---SudokuUI.exe （主要应用程序）  
