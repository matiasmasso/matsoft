Public Class Frm_Transmisio
    Private _Transmisio As DTOTransmisio
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOTransmisio)
        MyBase.New()
        Me.InitializeComponent()
        _Transmisio = value
    End Sub

    Private Sub Frm_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB2.Transmisio.Load(_Transmisio, exs) Then
            refresca()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub refresca()
        _AllowEvents = False
        With _Transmisio
            TextBoxId.Text = .Id
            TextBoxFch.Text = Format(.Fch.LocalDateTime, "dd/MM/yy HH:mm")
            TextBoxMgz.Text = .Mgz.Abr
            Xl_Deliveries1.Load(.Deliveries, Xl_Deliveries.Purposes.Transmisio)
            SetMenu()
            'ButtonOk.Enabled = .IsNew
            ButtonDel.Enabled = Not .IsNew
        End With
        _AllowEvents = True
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxMgz.TextChanged
        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        If Await FEB2.Transmisio.Update(_Transmisio, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Transmisio))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs, "error al desar la transmisió")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("retrocedim la transmisió?" & vbCrLf & "els albarans quedarán pendents de transmetre un altre cop", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If Await FEB2.Transmisio.Delete(_Transmisio, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Transmisio))
                Me.Close()
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub

    Private Sub SetMenu()
        Dim oMenu_Transmisio As New Menu_Transmisio({_Transmisio})
        AddHandler oMenu_Transmisio.AfterUpdate, AddressOf refreshrequest
        ArxiuToolStripMenuItem.DropDownItems.AddRange(oMenu_Transmisio.Range)
        ArxiuToolStripMenuItem.DropDownItems.Add("-")
        ArxiuToolStripMenuItem.DropDownItems.Add("refresca", Nothing, AddressOf RefreshRequest)
    End Sub

    Private Sub RefreshRequest(sender As Object, e As MatEventArgs)
        _Transmisio.IsLoaded = False
        Dim exs As New List(Of Exception)
        If FEB2.Transmisio.Load(_Transmisio, exs) Then
            refresca()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Xl_Deliveries1_RequestToRemove(sender As Object, e As MatEventArgs) Handles Xl_Deliveries1.RequestToRemove
        Dim oDelivery As DTODelivery = e.Argument
        _Transmisio.Deliveries.Remove(oDelivery)

        Dim exs As New List(Of Exception)
        If Await FEB2.Transmisio.Update(_Transmisio, exs) Then
            refresca()
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Transmisio))
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub
End Class


