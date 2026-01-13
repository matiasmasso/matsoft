

Public Class Frm_Vehicle
    Private mVehicle As Vehicle
    Private mModel As VehicleModel
    Private mDirtyModel As Boolean
    Private mAllowEvents As Boolean = False
    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Private Enum Tabs
        General
        Downloads
    End Enum

    Public Sub New(ByVal oVehicle As Vehicle)
        MyBase.new()
        Me.InitializeComponent()
        mVehicle = oVehicle
        Refresca()
        mAllowEvents = True
    End Sub

    Private Sub Refresca()
        With mVehicle
            mModel = .Model
            If mModel IsNot Nothing Then
                Xl_ContactConductor.Contact = .Conductor
                Xl_ContactVenedor.Contact = .Venedor
                TextBoxMarcaymodel.Text = mModel.MarcaiModel
                If .IsDonatDeBaixa Then
                    CheckBoxBaixa.Checked = True
                    DateTimePickerBaixa.Value = .Baixa
                    DateTimePickerBaixa.Visible = True
                Else
                    CheckBoxBaixa.Checked = False
                    DateTimePickerBaixa.Visible = False
                End If
            End If
            TextBoxMatricula.Text = .Matricula
            Xl_Lookup_Contracte1.Contract = .Contract
            Xl_Lookup_ContracteInsurance.Contract = .Insurance

            CheckBoxPrivat.Visible = BLL.BLLSession.Current.User.Rol.IsAdmin
            CheckBoxPrivat.Checked = .Privat

            DateTimePickerAlta.Value = .Alta
            If .BigFile IsNot Nothing Then
                Xl_BigFile1.BigFile = .BigFile.BigFile
            End If
            If .Model IsNot Nothing Then
                PictureBoxLogo.Image = .Model.Marca.Logo
            End If
        End With
    End Sub

    Private Sub CheckBoxBaixa_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxBaixa.Click
        DateTimePickerBaixa.Visible= CheckBoxBaixa.Checked
    End Sub

    Private Sub OnMarcaiModelChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        mModel = CType(sender, VehicleModel)
        TextBoxMarcaymodel.Text = mModel.MarcaiModel
        PictureBoxLogo.Image = mModel.Marca.Logo
        mDirtyModel = True
        ButtonOk.Enabled = True
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With mVehicle
            .Model = mModel
            .Matricula = TextBoxMatricula.Text
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
            If Xl_BigFile1.IsDirty Then
                .BigFile = New BigFileSrc(DTODocFile.Cods.Flota, .Guid, Xl_BigFile1.BigFile)
            End If
            .Update()

            RaiseEvent AfterUpdate(mVehicle, System.EventArgs.Empty)
            Me.Close()
        End With
    End Sub

    Private Sub ButtonBrowseMarca_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonBrowseMarca.Click
        Dim oFrm As New Frm_Vehicle_MarcasiModels(mModel)
        AddHandler oFrm.AfterUpdate, AddressOf OnMarcaiModelChanged
        oFrm.Show()
    End Sub

    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        TextBoxMatricula.TextChanged, _
        DateTimePickerAlta.ValueChanged, _
         DateTimePickerBaixa.ValueChanged, _
          CheckBoxBaixa.CheckedChanged, _
           Xl_ContactConductor.AfterUpdate, _
            Xl_ContactVenedor.AfterUpdate, _
             Xl_Lookup_Contracte1.AfterUpdate, _
              Xl_Lookup_ContracteInsurance.AfterUpdate, _
               Xl_BigFile1.AfterUpdate
        If mAllowEvents Then ButtonOk.Enabled = True
    End Sub


    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        Select Case CType(TabControl1.SelectedIndex, Tabs)
            Case Tabs.General
            Case Tabs.Downloads
                Static DoneDownloads As Boolean
                If Not DoneDownloads Then
                    DoneDownloads = True
                    'Dim OdOWNLOADS As ProductDownloads = mVehicle.DownloadTargets
                    'Xl_ProductDownloads1.Load(mVehicle.Guid, DownloadTarget.Cods.Vehicle)

                End If
        End Select
    End Sub
End Class