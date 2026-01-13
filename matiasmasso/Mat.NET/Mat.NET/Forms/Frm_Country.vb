Public Class Frm_Country
    Private _Country As DTOCountry
    Private _AllowEvent As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

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
        BLL.BLLCountry.Load(_Country)
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        With _Country
            TextBoxISO.Text = .ISO
            TextBoxNom_ESP.Text = .Nom_Esp
            TextBoxNom_CAT.Text = .Nom_Cat
            TextBoxNom_ENG.Text = .Nom_Eng
            CheckBoxCEE.Checked = .CEE
            TextBoxPrefixeTel.Text = .PrefixeTelefonic
            Xl_ImageFlag.Bitmap = .Flag
            ButtonOk.Enabled = .IsNew
            ButtonDel.Enabled = Not .IsNew
        End With
        _AllowEvent = True
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxISO.TextChanged, _
         TextBoxNom_ESP.TextChanged, _
          TextBoxNom_CAT.TextChanged, _
           TextBoxNom_ENG.TextChanged, _
            TextBoxPrefixeTel.TextChanged, _
             CheckBoxCEE.CheckedChanged, _
              Xl_ImageFlag.AfterUpdate

        If _AllowEvent Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _Country
            .ISO = TextBoxISO.Text
            .Nom_Esp = TextBoxNom_ESP.Text
            .Nom_Cat = TextBoxNom_CAT.Text
            .Nom_Eng = TextBoxNom_ENG.Text
            .PrefixeTelefonic = TextBoxPrefixeTel.Text
            .CEE = CheckBoxCEE.Checked
            .Flag = Xl_ImageFlag.Bitmap
        End With

        Dim exs As New List(Of Exception)
        If BLL.BLLCountry.Update(_Country, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Country))
            Me.Close()
        Else
            UIHelper.WarnError(exs, "error al desar")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("Eliminem " & _Country.Nom_Esp & "?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If BLL.BLLCountry.Delete(_Country, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Country))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
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
        Dim oZona As DTOZona = BLL.BLLZona.NewFromCountry(_Country)
        Dim oFrm As New Frm_Zona(oZona)
        AddHandler oFrm.AfterUpdate, AddressOf refrescaZonas
        oFrm.Show()
    End Sub

    Private Sub Xl_Zonas1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Zonas1.RequestToRefresh
        refrescaZonas()
    End Sub

    Private Sub refrescaZonas()
        Dim oZonas As List(Of DTOZona) = BLL.BLLZonas.All(_Country)
        Xl_Zonas1.Load(oZonas)
    End Sub

    Private Sub Xl_AreaProvincias1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_AreaProvincias1.RequestToAddNew
        Dim oAreaProvincia As DTOAreaProvincia = BLL.BLLAreaProvincia.NewFromCountry(_Country)
        Dim oFrm As New Frm_AreaProvincia(oAreaProvincia)
        AddHandler oFrm.AfterUpdate, AddressOf refrescaAreaProvincias
        oFrm.Show()
    End Sub

    Private Sub Xl_AreaProvincias1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_AreaProvincias1.RequestToRefresh
        refrescaAreaProvincias()
    End Sub

    Private Sub refrescaAreaProvincias()
        Dim oAreaProvincias As List(Of DTOAreaProvincia) = BLL.BLLAreaProvincias.All(_Country)
        Xl_AreaProvincias1.Load(oAreaProvincias)
    End Sub

    Private Sub Xl_AreaRegions1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_AreaRegions1.RequestToAddNew
        Dim oAreaRegio As DTOAreaRegio = BLL.BLLAreaRegio.NewFromCountry(_Country)
        Dim oFrm As New Frm_AreaRegio(oAreaRegio)
        AddHandler oFrm.AfterUpdate, AddressOf refrescaAreaRegions
        oFrm.Show()
    End Sub

    Private Sub Xl_AreaRegions1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_AreaRegions1.RequestToRefresh
        refrescaAreaRegions()
    End Sub

    Private Sub refrescaAreaRegions()
        Dim oAreaRegions As List(Of DTOAreaRegio) = BLL.BLLAreaRegions.All(_Country)
        Xl_AreaRegions1.Load(oAreaRegions)
    End Sub
End Class