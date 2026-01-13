Public Class Frm_DeliveryTraspas
    Private _DeliveryTraspas As DTODeliveryTraspas
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTODeliveryTraspas)
        MyBase.New()
        Me.InitializeComponent()
        _DeliveryTraspas = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB2.DeliveryTraspas.Load(_DeliveryTraspas, exs) Then
            With _DeliveryTraspas
                TextBoxId.Text = .Id
                DateTimePicker1.Value = .Fch
                Xl_ContactFrom.Contact = .MgzFrom
                Xl_ContactTo.Contact = .MgzTo
                Xl_DeliveryTraspasItems1.Load(.Items)
                'ButtonOk.Enabled = .IsNew
                'ButtonDel.Enabled = Not .IsNew
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
            Me.Close()
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxId.TextChanged
        If _AllowEvents Then
            'ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs)
        With _DeliveryTraspas
            .Fch = DateTimePicker1.Value
            .Id = TextBoxId.Text
            .MgzFrom = DTOMgz.FromContact(Xl_ContactFrom.Contact)
            .MgzTo = DTOMgz.FromContact(Xl_ContactTo.Contact)
        End With

        Dim exs As New List(Of Exception)
        If Await FEB2.DeliveryTraspas.Update(_DeliveryTraspas, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_DeliveryTraspas))
            Me.Close()
        Else
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
            If Await FEB2.DeliveryTraspas.Delete(_DeliveryTraspas, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_DeliveryTraspas))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub


End Class

