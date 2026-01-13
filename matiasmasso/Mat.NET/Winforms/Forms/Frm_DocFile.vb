Public Class Frm_DocFile

    Private _DocFile As DTODocFile

    Public Event AfterUpdate(sender As Object, e As System.EventArgs)

    Public Sub New(oDocFile As DTODocFile)
        MyBase.New()
        Me.InitializeComponent()
        _DocFile = oDocFile
    End Sub

    Private Async Sub Frm_DocFile_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        TextBoxHash.Text = _DocFile.Hash
        Await Xl_DocFile1.Load(_DocFile)

        Dim oLogs = Await FEB2.DocFile.Logs(_DocFile, exs)
        If exs.Count = 0 Then
            Xl_DocFile_Logs1.DataSource = oLogs
        Else
            UIHelper.WarnError(exs)
        End If

        ButtonDel.Enabled = True

        Dim oSrcs = Await FEB2.DocFile.Srcs(_DocFile, exs)
        If exs.Count = 0 Then
            Xl_DocfileSrcs1.Load(oSrcs)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        If Await FEB2.DocFile.Upload(Xl_DocFile1.Value, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(Xl_DocFile1.Value))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_DocFile1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_DocFile1.AfterUpdate
        Dim oDocfile As DTODocFile = e.Argument
        TextBoxHash.Text = oDocfile.Hash
        ButtonOk.Enabled = True
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        If Await FEB2.DocFile.Delete(_DocFile, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_DocFile))
            Me.Close()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub


End Class