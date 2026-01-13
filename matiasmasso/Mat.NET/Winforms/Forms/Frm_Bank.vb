Public Class Frm_Bank
    Private _Bank As DTOBank
    Private LoadedAgcs As Boolean
    Private LoadedGroup As Boolean
    Private LoadedCtas As Boolean

    Private _AllowEvents As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Private Enum Tabs
        Gral
        Agcs
        Group
        Ibans
    End Enum

    Public Sub New(oBank As DTOBank)
        MyBase.New()
        Me.InitializeComponent()
        _Bank = oBank
    End Sub

    Private Sub Frm_Bank_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB2.Bank.Load(_Bank, exs) Then
            Refresca()
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Refresca()
        With _Bank
            TextBoxBankCod.Text = .Id
            TextBoxPais.Text = DTOCountry.NomTraduit(.Country, Current.Session.Lang)
            TextBoxBankNom.Text = .RaoSocial
            TextBoxBankAlias.Text = .NomComercial
            TextBoxBankTel.Text = .Tel
            If .Swift IsNot Nothing Then
                TextBoxBankSwift.Text = .Swift
            End If
            TextBoxBankWeb.Text = .web
            PictureBoxBrowse.Enabled = (TextBoxBankWeb.Text.isNotEmpty())
            Xl_ImageLogo.Load(.logo, 48, 48)
            CheckBoxObsoleto.Checked = .Obsoleto
            CheckBoxSEPAB2B.Checked = .SEPAB2B
            ButtonDel.Enabled = Not .isnew
        End With

    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        UIHelper.ToggleProggressBar(Panel1, True)
        With _Bank
            .id = TextBoxBankCod.Text
            .raoSocial = TextBoxBankNom.Text
            .nomComercial = TextBoxBankAlias.Text
            .tel = TextBoxBankTel.Text
            .swift = TextBoxBankSwift.Text
            .web = TextBoxBankWeb.Text
            .logo = LegacyHelper.ImageHelper.Converter(Xl_ImageLogo.Bitmap)
            .SEPAB2B = CheckBoxSEPAB2B.Checked
            .obsoleto = CheckBoxObsoleto.Checked
        End With

        Dim exs As New List(Of Exception)
        If Await FEB2.Bank.Update(_Bank, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Bank))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs, "error al desar la entitat bancària")
        End If
    End Sub

    Private Async Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        Dim exs As New List(Of Exception)
        Select Case TabControl1.SelectedIndex
            Case Tabs.Agcs
                If _AllowEvents And Not LoadedAgcs Then
                    LoadBranches()
                    LoadedAgcs = True
                End If
            Case Tabs.Group
                If _AllowEvents And Not LoadedGroup Then
                    Dim oBankGroup = Await FEB2.BankGroup.FromSameBank(_Bank, exs)
                    If exs.Count = 0 Then
                        Xl_Banks1.Load(oBankGroup.Banks, Nothing, DTO.Defaults.SelectionModes.browse)
                        LoadedGroup = True
                    Else
                        UIHelper.WarnError(exs)
                    End If
                End If
            Case Tabs.Ibans
                If _AllowEvents And Not LoadedCtas Then
                    Dim oIbans = Await FEB2.Ibans.FromBank(exs, Current.Session.Emp, _Bank)
                    Xl_Ibans1.Load(oIbans)
                    LoadedCtas = True
                End If
        End Select
    End Sub


    Private Sub TextBox_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        TextBoxBankCod.TextChanged,
        CheckBoxObsoleto.CheckedChanged,
        TextBoxBankTel.TextChanged,
        TextBoxBankAlias.TextChanged,
        TextBoxBankNom.TextChanged,
         CheckBoxSEPAB2B.CheckedChanged

        SetDirty()
    End Sub

    Private Sub Xl_ImageLogo_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_ImageLogo.AfterUpdate
        SetDirty()
    End Sub

    Private Sub SetDirty()
        If _AllowEvents Then
            Dim Enable As Boolean = True
            If TextBoxBankCod.Text = "" Then Enable = False
            If TextBoxBankNom.Text = "" Then Enable = False
            ButtonOk.Enabled = Enable
        End If
    End Sub


    Private Async Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("eliminem el banc" & vbCrLf & DTOBank.NomComercialORaoSocial(_Bank) & "?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB2.Bank.Delete(_Bank, exs) Then
                MsgBox("banc eliminat", MsgBoxStyle.Information, "MAT.NET")
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar la entitat bancària")
            End If
        Else
            MsgBox("Operacio cancelada per l'usuari", MsgBoxStyle.Exclamation, "MAT.NET")
        End If
    End Sub

    Private Sub TextBoxBankWeb_TextChanged(sender As Object, e As System.EventArgs) Handles TextBoxBankWeb.TextChanged
        If _AllowEvents Then
            Dim BlValidUrl As Boolean = TextHelper.IsValidUrl(TextBoxBankWeb.Text)
            PictureBoxBrowse.Enabled = BlValidUrl
            SetDirty()
        End If
    End Sub

    Private Sub PictureBoxBrowse_Click(sender As Object, e As System.EventArgs) Handles PictureBoxBrowse.Click
        UIHelper.ShowHtml(TextBoxBankWeb.Text)
    End Sub

    Private Sub Xl_BankBranches1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_BankBranches1.RequestToAddNew
        Dim oBranch As DTOBankBranch = DTOBankBranch.Factory(Xl_Banks1.Value)
        Dim oFrm As New Frm_BankBranch(oBranch)
        AddHandler oFrm.AfterUpdate, AddressOf LoadBranches
        oFrm.Show()
    End Sub

    Private Async Sub LoadBranches()
        Dim exs As New List(Of Exception)
        Dim oBranches = Await FEB2.BankBranches.All(_Bank, exs)
        If exs.Count = 0 Then
            Xl_BankBranches1.Load(oBranches, Nothing, DTO.Defaults.SelectionModes.Browse)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub TextBoxBankSwift_TextChanged(sender As Object, e As EventArgs) Handles TextBoxBankSwift.TextChanged
        Dim src As String = TextBoxBankSwift.Text
        If src = "" Then
            PictureBoxSwiftValidation.Image = Nothing
        ElseIf DTOBank.ValidateBIC(src) Then
            PictureBoxSwiftValidation.Image = My.Resources.vb
        Else
            PictureBoxSwiftValidation.Image = My.Resources.warning
        End If
        SetDirty()
    End Sub

    Private Async Sub Xl_BankBranches1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_BankBranches1.RequestToRefresh
        Dim exs As New List(Of Exception)
        Dim oBranches = Await FEB2.BankBranches.All(_Bank, exs)
        If exs.Count = 0 Then
            Xl_BankBranches1.Load(oBranches, e.Argument, DTO.Defaults.SelectionModes.Browse)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub


End Class