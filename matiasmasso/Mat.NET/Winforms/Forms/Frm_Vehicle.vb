Public Class Frm_Vehicle
    Private _Vehicle As DTOVehicle
    Private _Model As DTOVehicleModel
    Private mDirtyModel As Boolean
    Private mAllowEvents As Boolean = False
    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Private Enum Tabs
        General
        Downloads
        Multes
    End Enum

    Public Sub New(ByVal oVehicle As DTOVehicle)
        MyBase.New()
        Me.InitializeComponent()
        _Vehicle = oVehicle
        Refresca()
        mAllowEvents = True
    End Sub

    Private Async Sub Refresca()
        Dim exs As New List(Of Exception)
        If FEB2.Vehicle.Load(_Vehicle, exs) Then
            With _Vehicle
                _Model = .Model
                If _Model IsNot Nothing Then
                    Xl_ContactConductor.Contact = .Conductor
                    Xl_ContactVenedor.Contact = .Venedor
                    Xl_LookupVehicleMarcayModel1.VehicleModelValue = _Model
                    If _Vehicle.IsDonatDeBaixa Then
                        CheckBoxBaixa.Checked = True
                        DateTimePickerBaixa.Value = .Baixa
                        DateTimePickerBaixa.Visible = True
                    Else
                        CheckBoxBaixa.Checked = False
                        DateTimePickerBaixa.Visible = False
                    End If
                End If
                TextBoxMatricula.Text = .Matricula
                TextBoxBastidor.Text = .Bastidor
                Xl_Lookup_Contracte1.Contract = .Contract
                Xl_Lookup_ContracteInsurance.Contract = .Insurance

                CheckBoxPrivat.Visible = Current.Session.User.Rol.IsAdmin
                CheckBoxPrivat.Checked = .Privat

                If .Alta > DateTimePickerAlta.MinDate Then
                    DateTimePickerAlta.Value = .Alta
                End If
                Xl_Image1.Bitmap = LegacyHelper.ImageHelper.Converter(.image)
                If .Model IsNot Nothing Then
                    'PictureBoxLogo.Image = .Model.Marca.Logo
                End If

                Dim oDownloads = Await FEB2.ProductDownloads.All(_Vehicle, exs)
                If exs.Count = 0 Then
                    Xl_ProductDownloads1.Load(oDownloads)
                Else
                    UIHelper.WarnError(exs)
                End If

                ButtonDel.Enabled = Not .IsNew
            End With
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub CheckBoxBaixa_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        DateTimePickerBaixa.Visible = CheckBoxBaixa.Checked
    End Sub

    Private Async Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With _Vehicle
            .Model = Xl_LookupVehicleMarcayModel1.VehicleModelValue
            .Matricula = TextBoxMatricula.Text
            .Bastidor = TextBoxBastidor.Text
            .Conductor = Xl_ContactConductor.Contact
            .Venedor = Xl_ContactVenedor.Contact
            .Alta = DateTimePickerAlta.Value
            .Contract = Xl_Lookup_Contracte1.Contract
            .Insurance = Xl_Lookup_ContracteInsurance.Contract
            .Privat = CheckBoxPrivat.Checked
            If CheckBoxBaixa.Checked Then
                .Baixa = DateTimePickerBaixa.Value
            Else
                .Baixa = Date.MinValue
            End If
            If Xl_Image1.IsDirty Then
                .image = LegacyHelper.ImageHelper.Converter(Xl_Image1.Bitmap)
            End If

            Dim exs As New List(Of Exception)
            UIHelper.ToggleProggressBar(Panel1, True)
            If Await FEB2.Vehicle.Upload(_Vehicle, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Vehicle))
                Me.Close()
            Else
                UIHelper.ToggleProggressBar(Panel1, False)
                UIHelper.WarnError(exs)
            End If

        End With
    End Sub

    Private Sub ButtonBrowseMarca_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        'Dim oFrm As New Frm_Vehicle_MarcasiModels(_Model)
        'AddHandler oFrm.AfterUpdate, AddressOf OnMarcaiModelChanged
        'oFrm.Show()
    End Sub

    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
            TextBoxMatricula.TextChanged,
             TextBoxBastidor.TextChanged,
        DateTimePickerAlta.ValueChanged,
         DateTimePickerBaixa.ValueChanged,
          CheckBoxBaixa.CheckedChanged,
           Xl_LookupVehicleMarcayModel1.AfterUpdate,
           Xl_ContactConductor.AfterUpdate,
            Xl_ContactVenedor.AfterUpdate,
             Xl_Lookup_Contracte1.AfterUpdate,
              Xl_Lookup_ContracteInsurance.AfterUpdate,
               Xl_Image1.AfterUpdate

        If mAllowEvents Then ButtonOk.Enabled = True
    End Sub


    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        Select Case DirectCast(TabControl1.SelectedIndex, Tabs)
            Case Tabs.General
            Case Tabs.Downloads
                Static DoneDownloads As Boolean
                If Not DoneDownloads Then
                    DoneDownloads = True
                    LoadProductDownloads()
                End If
            Case Tabs.Multes
                Static DoneMultes As Boolean
                If Not DoneMultes Then
                    DoneMultes = True
                    refrescaMultes()
                End If
        End Select
    End Sub

    Private Async Sub LoadProductDownloads()
        Dim exs As New List(Of Exception)
        Dim oDownloads = Await FEB2.ProductDownloads.All(_Vehicle, exs)
        If exs.Count = 0 Then
            Xl_ProductDownloads1.Load(oDownloads)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_ProductDownloads1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_ProductDownloads1.RequestToAddNew
        Dim oProductDownload As New DTOProductDownload
        Dim exs As New List(Of Exception)

        If oProductDownload.AddTarget(_Vehicle, exs) Then
            Dim oFrm As New Frm_ProductDownload(oProductDownload)
            AddHandler oFrm.AfterUpdate, AddressOf LoadProductDownloads
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Xl_ProductDownloads1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_ProductDownloads1.RequestToRefresh
        Dim exs As New List(Of Exception)
        Dim oDownloads = Await FEB2.ProductDownloads.All(_Vehicle, exs)
        If exs.Count = 0 Then
            Xl_ProductDownloads1.Load(oDownloads)
        Else
            UIHelper.WarnError(exs)
        End If

    End Sub

#Region "Multes"

    Private Async Sub refrescaMultes()
        Dim exs As New List(Of Exception)
        Dim oMultes = Await FEB2.Multes.All(_Vehicle, exs)
        If exs.Count = 0 Then
            Xl_Multas1.Load(oMultes)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_Multas1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Multas1.RequestToRefresh
        refrescaMultes()
    End Sub

    Private Sub Xl_Multas1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Multas1.RequestToAddNew
        Dim oMulta As New DTOMulta
        With oMulta
            .Subjecte = _Vehicle
            .Fch = Today
        End With
        Dim oFrm As New Frm_Multa(oMulta)
        AddHandler oFrm.AfterUpdate, AddressOf refrescaMultes
        oFrm.Show()
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim rc = MsgBox("Eliminem aquest vehicle?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB2.Vehicle.Delete(_Vehicle, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Vehicle))
                Me.Close()
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub
#End Region
End Class