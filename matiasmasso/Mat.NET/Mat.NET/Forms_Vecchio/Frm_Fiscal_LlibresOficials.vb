
Imports System.Data.SqlClient
Imports Excel = Microsoft.Office.Interop.Excel

Public Class Frm_Fiscal_LlibresOficials
    Private mLang As New DTOLang("CAT")

    Public Enum FileFormats
        NotSet
        Csv
        Pdf
    End Enum

    Private Sub Frm_Fiscal_LlibresOficials_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        NumericUpDownYea.Value = Today.Year - 1
        LoadMesos()
    End Sub

    Private Sub LoadMesos()
        Dim sMesos As String()
        ReDim sMesos(12)
        sMesos(0) = "INI"
        Dim i As Integer
        For i = 1 To 12
            sMesos(i) = mLang.MesAbr(i)
        Next
        ComboBoxMes.Items.AddRange(sMesos)
        ComboBoxMes.SelectedIndex = 12
    End Sub

    Private Sub ButtonRenum_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonRenum.Click
        Dim oExercici As Exercici = CurrentExercici()
        Dim exs as New List(Of exception)
        Cursor = Cursors.WaitCursor
        If ExerciciLoader.RenumeraAssentaments(oExercici, exs) Then
            Cursor = Cursors.Default
            MsgBox("registres renumerats")
        Else
            Cursor = Cursors.Default
            UIHelper.WarnError( exs, "error al renumerar les assentaments")
        End If
    End Sub


    Private Sub ButtonDiari_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDiari.Click
        Dim oLlibre As New Llibre_Diari(CurrentExercici)
        SaveFile(oLlibre, FileFormats.Pdf)
    End Sub

    Private Sub ButtonDiariExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonDiariExcel.Click
        Dim oLlibre As New Llibre_Diari(CurrentExercici)
        SaveFile(oLlibre, FileFormats.Csv)
    End Sub

    Private Sub ButtonMajor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonMajor.Click
        Dim oExercici As Exercici = CurrentExercici()
        Dim oLlibre As New Llibre_Major(oExercici.Emp, oExercici.FchEnd)
        SaveFile(oLlibre, FileFormats.Pdf)
    End Sub

    Private Sub ButtonMajorExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonMajorExcel.Click
        Dim oExercici As Exercici = CurrentExercici()
        Dim oLlibre As New Llibre_Major(oExercici.Emp, oExercici.FchEnd)
        SaveFile(oLlibre, FileFormats.Csv)
    End Sub


    Private Sub ButtonFresEmeses_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonFresEmeses.Click
        Dim oLlibre As New Llibre_IVAFacturesEmeses(CurrentExercici)
        SaveFile(oLlibre, FileFormats.Csv)
    End Sub

    Private Sub ButtonLlibreAlbs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonLlibreAlbs.Click
        Dim oLlibre As New Llibre_Albarans(CurrentExercici)
        SaveFile(oLlibre, FileFormats.Csv)
    End Sub

    Private Sub onNewRow(ByVal sender As Object, ByVal e As System.EventArgs)
        ProgressBar1.Increment(1)
        Application.DoEvents()
    End Sub

    Private Sub onNewPage(ByVal sender As Object, ByVal e As System.EventArgs)
        TextBoxStatusBar.Text = "pag. " & sender.ToString
        Application.DoEvents()
    End Sub

    Private Sub LaunchProgressBar(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim iRowsCount As Integer = CInt(sender)
        With ProgressBar1
            .Minimum = 0
            .Maximum = iRowsCount
            .Visible = True
        End With
        Cursor = Cursors.Default
        Application.DoEvents()
    End Sub

    Private Sub ButtonPGC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonPGC.Click
        root.ShowPGC()
    End Sub



    Private Function CurrentExercici() As Exercici
        Dim retVal As New Exercici(BLL.BLLApp.Emp, NumericUpDownYea.Value)
        Return retVal
    End Function

    Private Sub SaveFile(ByVal oLlibre As Llibre_Comptable, ByVal oFormat As FileFormats)
        Dim oExercici As Exercici = oLlibre.Exercici
        Dim sExtension As String = oFormat.ToString
        Dim oDlg As New SaveFileDialog
        With oDlg
            .DefaultExt = sExtension
            .AddExtension = True
            .FileName = MaxiSrvr.Emp.FromDTOEmp(oExercici.Emp).Org.NIF & "." & oExercici.Yea.ToString & "." & oLlibre.FilenameRoot & "." & sExtension
            .InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
            .Title = "guardar " & oLlibre.Concepte & " a " & MaxiSrvr.Emp.FromDTOEmp(oExercici.Emp).Org.Nom
            Dim Rc As DialogResult = .ShowDialog()
            If Rc = Windows.Forms.DialogResult.OK Then
                Cursor = Cursors.WaitCursor

                AddHandler oLlibre.onDataRead, AddressOf LaunchProgressBar
                AddHandler oLlibre.onNewRow, AddressOf onNewRow
                AddHandler oLlibre.onNewPage, AddressOf onNewPage
                oLlibre.Save(.FileName)
                MsgBox(oLlibre.Concepte & " grabat a " & .FileName)
                ProgressBar1.Visible = False
            End If
        End With

    End Sub


    Private Sub ButtonBalanç_Click(sender As Object, e As EventArgs) Handles ButtonBalanç.Click

    End Sub
End Class