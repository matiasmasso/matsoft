Public Class Frm_InvoiceReceived

    Private _value As DTOInvoiceReceived
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOInvoiceReceived)
        MyBase.New
        InitializeComponent()
        _value = value
    End Sub

    Private Sub Frm_InvoiceReceived_Load(sender As Object, e As EventArgs) Handles Me.Load
        refresca()
        _AllowEvents = True
    End Sub

    Private Sub refresca()
        Dim exs As New List(Of Exception)
        If FEB.InvoiceReceived.Load(exs, _value) Then
            With _value
                If .Exceptions.Count = 0 Then
                    SplitContainer1.Panel1Collapsed = True
                End If
                If _value.Proveidor IsNot Nothing Then
                    TextBoxProveidor.Text = _value.Proveidor.Nom
                End If
                TextBoxDocNum.Text = .DocNum
                If _value.Fch > DateTimePicker1.MinDate Then DateTimePicker1.Value = .Fch
                Xl_Exceptions1.Load(.Exceptions)
                Xl_InvoiceReceivedItems1.Load(_value)
            End With
        Else
            UIHelper.WarnError(exs)
            Me.Close()
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub RetrocedirToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RetrocedirToolStripMenuItem.Click
        Await Do_Delete()
    End Sub

    Private Async Function Do_Delete() As Task
        Dim rc As MsgBoxResult = MsgBox("Retrocedim aquesta factura?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB.InvoiceReceived.Delete(exs, _value) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs)
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al retrocedir la factura")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Function

    Private Sub Xl_InvoiceReceivedItems1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_InvoiceReceivedItems1.RequestToRefresh
        _value.IsLoaded = False
        RaiseEvent AfterUpdate(Me, New MatEventArgs)
        refresca()
    End Sub

    Private Async Sub ValidateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ValidateToolStripMenuItem.Click
        Dim exs As New List(Of Exception)
        If Await FEB.InvoiceReceived.Update(exs, _value) Then
            _value.IsLoaded = False
            refresca()
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_value))
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Dim exs As New List(Of Exception)
        With _value
            .DocNum = TextBoxDocNum.Text
            .Fch = DateTimePicker1.Value
        End With
        If Await FEB.InvoiceReceived.Update(exs, _value) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_value))
            Me.Close()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub ControlChanged(sender As Object, e As EventArgs) Handles TextBoxDocNum.TextChanged, DateTimePicker1.ValueChanged
        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc = MsgBox("Eliminem aquesta factura?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If Await FEB.InvoiceReceived.Delete(exs, _value) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_value))
                Me.Close()
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub
End Class