Public Class Frm_TpvLog

    Private _TpvLog As DTOTpvLog

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(oLog As DTOTpvLog)
        MyBase.New
        InitializeComponent()
        _TpvLog = oLog
    End Sub

    Private Async Sub Frm_TpvLog_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        With _TpvLog
            TextBoxDsOrder.Text = .Ds_Order
            TextBoxFchCreated.Text = Format(.FchCreated, "dd/MM/yyyy HH:mm")
            TextBoxDsFch.Text = .Ds_Date & " " & .Ds_Hour
            TextBoxTitular.Text = Await FEB2.TpvLog.TitularNom(_TpvLog, exs)
            TextBoxConcept.Text = FEB2.TpvLog.Purpose(exs, _TpvLog)
            TextBoxDsAmt.Text = DTOAmt.CurFormatted(DTOTpvLog.Amt(_TpvLog))
            TextBoxDsAuthorisation.Text = .Ds_AuthorisationCode
            TextBoxExceptions.Text = .Exceptions
            If IsNumeric(.Ds_AuthorisationCode) Then
                TextBoxDsAmt.BackColor = Color.LightGreen
            End If
        End With
        If exs.Count > 0 Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        If Await FEB2.TpvLog.Delete(_TpvLog, exs) Then
            RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
            Me.Close()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub
End Class