Public Class Frm_DocFile

    Private _DocFile As DTODocFile

    Public Event AfterUpdate(sender As Object, e As System.EventArgs)

    Public Sub New(oDocFile As DTODocFile)
        MyBase.New()
        Me.InitializeComponent()
        _DocFile = oDocFile
        Xl_DocFile1.Load(_DocFile)
        Xl_DocFile_Srcs1.DataSource = _DocFile.Srcs
        Xl_DocFile_Logs1.DataSource = BLL.BLLDocFile.Logs(_DocFile)
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub
End Class