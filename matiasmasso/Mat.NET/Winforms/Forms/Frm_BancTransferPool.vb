Public Class Frm_BancTransferPool

    Private _BancTransferPool As DTOBancTransferPool
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOBancTransferPool)
        MyBase.New()
        Me.InitializeComponent()
        _BancTransferPool = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB2.BancTransferPool.Load(_BancTransferPool, exs) Then
            With _BancTransferPool
                TextBoxFch.Text = .Cca.Fch.ToShortDateString
                TextBoxBancEmissor.Text = .BancEmissor.Abr
                TextBoxCca.Text = DTOCca.FullNom(.Cca)
                TextBoxRef.Text = .Ref
                TextBoxTotal.Text = DTOAmt.CurFormatted(_BancTransferPool.Total())
                Xl_BancTransferPools1.Load(_BancTransferPool)
                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With
            Dim oMenu As New Menu_BancTransferPool(_BancTransferPool)
            ArxiuToolStripMenuItem.DropDownItems.AddRange(oMenu.Range)
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxFch.TextChanged
        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        UIHelper.ToggleProggressBar(Panel1, True)
        With _BancTransferPool
            .Ref = TextBoxRef.Text
        End With

        Dim exs As New List(Of Exception)
        If Await FEB2.BancTransferPool.SaveRef(_BancTransferPool, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_BancTransferPool))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs, "error al desar")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If Await FEB2.BancTransferPool.Delete(_BancTransferPool, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_BancTransferPool))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub

    Private Sub TextBoxRef_TextChanged(sender As Object, e As EventArgs) Handles TextBoxRef.TextChanged
        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub
End Class


