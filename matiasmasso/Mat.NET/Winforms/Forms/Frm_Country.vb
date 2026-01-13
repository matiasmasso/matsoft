Public Class Frm_Country
    Private _Country As DTOCountry
    Private _AllowEvent As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)
    Public Event AfterDelete(sender As Object, e As MatEventArgs)

    Private Enum Tabs
        General
        Bancs
        Zonas
        Provincies
        Regions
    End Enum

    Public Sub New(value As DTOCountry)
        MyBase.New()
        Me.InitializeComponent()
        _Country = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB2.Country.Load(_Country, exs) Then
            With _Country
                TextBoxISO.Text = .ISO
                TextBoxNom_ESP.Text = .LangNom.Esp
                TextBoxNom_CAT.Text = .LangNom.Cat
                TextBoxNom_ENG.Text = .LangNom.Eng
                ComboBoxExportCod.SelectedIndex = .ExportCod
                TextBoxPrefixeTel.Text = .PrefixeTelefonic
                Xl_Langs1.Value = .lang
                Xl_ImageFlag.Bitmap = LegacyHelper.ImageHelper.Converter(.flag)
                ButtonOk.Enabled = True ' .IsNew
                ButtonDel.Enabled = True ' Not .IsNew
            End With
            _AllowEvent = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Control_Changed() Handles _
        TextBoxISO.TextChanged,
         TextBoxNom_ESP.TextChanged,
          TextBoxNom_CAT.TextChanged,
           TextBoxNom_ENG.TextChanged,
            TextBoxPrefixeTel.TextChanged,
             ComboBoxExportCod.SelectedIndexChanged,
              Xl_ImageFlag.AfterUpdate,
                Xl_Langs1.AfterUpdate

        If _AllowEvent Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        UIHelper.ToggleProggressBar(Panel1, True)
        With _Country
            .ISO = TextBoxISO.Text
            .LangNom.esp = TextBoxNom_ESP.Text
            .LangNom.cat = TextBoxNom_CAT.Text
            .LangNom.eng = TextBoxNom_ENG.Text
            .prefixeTelefonic = TextBoxPrefixeTel.Text
            .exportCod = ComboBoxExportCod.SelectedIndex
            .lang = Xl_Langs1.Value
            .flag = LegacyHelper.ImageHelper.Converter(Xl_ImageFlag.Bitmap)
        End With

        Dim exs As New List(Of Exception)
        If Await FEB2.Country.Update(_Country, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Country))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("Eliminem " & _Country.LangNom.Esp & "?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then

            Dim exs As New List(Of Exception)
            If Await FEB2.Country.Delete(_Country, exs) Then
                RaiseEvent AfterDelete(Me, New MatEventArgs(_Country))
                Me.Close()
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Select Case TabControl1.SelectedIndex
            Case Tabs.Zonas
                Static Done2 As Boolean
                If Not Done2 Then
                    refrescaZonas()
                    Done2 = True
                End If
            Case Tabs.Provincies
                Static Done3 As Boolean
                If Not Done3 Then
                    refrescaAreaProvincias()
                    Done3 = True
                End If
            Case Tabs.Regions
                Static Done4 As Boolean
                If Not Done4 Then
                    refrescaAreaRegions()
                    Done4 = True
                End If
        End Select
    End Sub

    Private Sub Xl_Zonas1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Zonas1.RequestToAddNew
        Dim oZona As DTOZona = DTOZona.Factory(_Country)
        Dim oFrm As New Frm_Zona(oZona)
        AddHandler oFrm.AfterUpdate, AddressOf refrescaZonas
        oFrm.Show()
    End Sub

    Private Sub Xl_Zonas1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Zonas1.RequestToRefresh
        refrescaZonas()
    End Sub

    Private Async Sub refrescaZonas()
        Dim exs As New List(Of Exception)
        Dim oZonas = Await FEB2.Zonas.All(_Country, exs)
        If exs.Count = 0 Then
            Xl_Zonas1.Load(oZonas)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_AreaProvincias1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_AreaProvincias1.RequestToAddNew
        Dim oAreaProvincia = DTOAreaProvincia.Factory(_Country)
        Dim oFrm As New Frm_AreaProvincia(oAreaProvincia)
        AddHandler oFrm.AfterUpdate, AddressOf refrescaAreaProvincias
        oFrm.Show()
    End Sub

    Private Sub Xl_AreaProvincias1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_AreaProvincias1.RequestToRefresh
        refrescaAreaProvincias()
    End Sub

    Private Async Sub refrescaAreaProvincias()
        Dim exs As New List(Of Exception)
        Dim oAreaProvincias = Await FEB2.AreaProvincias.All(_Country, exs)
        If exs.Count = 0 Then
            Xl_AreaProvincias1.Load(oAreaProvincias)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_AreaRegions1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_AreaRegions1.RequestToAddNew
        Dim oAreaRegio = DTOAreaRegio.Factory(_Country)
        Dim oFrm As New Frm_AreaRegio(oAreaRegio)
        AddHandler oFrm.AfterUpdate, AddressOf refrescaAreaRegions
        oFrm.Show()
    End Sub

    Private Sub Xl_AreaRegions1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_AreaRegions1.RequestToRefresh
        refrescaAreaRegions()
    End Sub

    Private Async Sub refrescaAreaRegions()
        Dim exs As New List(Of Exception)
        Dim oAreaRegions = Await FEB2.AreaRegions.All(_Country, exs)
        If exs.Count = 0 Then
            Xl_AreaRegions1.Load(oAreaRegions)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub


End Class