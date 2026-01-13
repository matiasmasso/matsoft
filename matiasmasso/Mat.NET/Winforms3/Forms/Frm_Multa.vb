Public Class Frm_Multa


    Private _Multa As DTOMulta
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Enum Tabs
        General
        Downloads
    End Enum

    Public Sub New(value As DTOMulta)
        MyBase.New()
        Me.InitializeComponent()
        _Multa = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.Multa.Load(_Multa, exs) Then

            With _Multa
                Xl_Contact2Emisor.Contact = .Emisor
                TextBoxExpedient.Text = .Expedient
                Try
                    Dim oVehicle As New DTOVehicle(_Multa.Subjecte.Guid)
                    oVehicle.Nom = _Multa.Subjecte.Nom
                    Xl_LookupVehicle1.Vehicle = oVehicle
                Catch ex As Exception
                    Stop
                End Try
                If .Fch > DateTimePickerFch.MinDate Then DateTimePickerFch.Value = .Fch
                If .Vto > DateTimePickerVto.MinDate Then DateTimePickerVto.Value = .Vto
                If .Pagat > DateTimePickerPagat.MinDate Then
                    CheckBoxPagat.Checked = True
                    DateTimePickerPagat.Value = .Pagat
                    DateTimePickerPagat.Visible = True
                End If
                Xl_Amount1.Amt = .Amt
                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        Xl_Contact2Emisor.AfterUpdate,
         TextBoxExpedient.TextChanged,
          DateTimePickerFch.ValueChanged,
           DateTimePickerVto.ValueChanged,
            DateTimePickerPagat.ValueChanged,
             Xl_Amount1.AfterUpdate,
              Xl_LookupVehicle1.AfterUpdate

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub CheckBoxPagat_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxPagat.CheckedChanged
        If _AllowEvents Then
            DateTimePickerPagat.Visible = CheckBoxPagat.Checked
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _Multa
            .Emisor = Xl_Contact2Emisor.Contact
            .Expedient = TextBoxExpedient.Text
            .Subjecte = Xl_LookupVehicle1.Vehicle
            .Fch = DateTimePickerFch.Value
            .Vto = DateTimePickerVto.Value
            If CheckBoxPagat.Checked Then
                .Pagat = DateTimePickerPagat.Value
            Else
                .Pagat = Nothing
            End If
            .Amt = Xl_Amount1.Amt
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        If Await FEB.Multa.Update(_Multa, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Multa))
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
            If Await FEB.Multa.Delete(_Multa, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Multa))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Select Case TabControl1.SelectedIndex
            Case Tabs.Downloads
                Static LoadedDocFileSrcs As Boolean
                If Not LoadedDocFileSrcs Then
                    refrescaDocFileSrcs()
                End If
        End Select
    End Sub

    Private Async Sub refrescaDocFileSrcs()
        Dim exs As New List(Of Exception)
        Dim oDocFileSrcs = Await FEB.DocFileSrcs.All(_Multa, exs)
        If exs.Count = 0 Then
            Xl_DocfileSrcs1.Load(oDocFileSrcs)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_DocfileSrcs1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_DocfileSrcs1.RequestToRefresh
        refrescaDocFileSrcs()
    End Sub

    Private Sub Xl_DocfileSrcs1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_DocfileSrcs1.RequestToAddNew
        Dim oDocfileSrc As New DTODocFileSrc()
        With oDocfileSrc
            .Cod = DTODocFile.Cods.Download
            .Src = _Multa
        End With
        Dim oFrm As New Frm_DocfileSrc(oDocfileSrc)
        AddHandler oFrm.AfterUpdate, AddressOf refrescaDocFileSrcs
        oFrm.Show()
    End Sub
End Class


