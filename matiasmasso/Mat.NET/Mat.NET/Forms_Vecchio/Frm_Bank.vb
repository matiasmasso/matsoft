Public Class Frm_Bank
    Private _Bank As DTOBank
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
        BLL.BLLBank.Load(_Bank)
        Refresca()
        _AllowEvents = True
    End Sub

    Private Sub Refresca()
        With _Bank
            TextBoxBankCod.Text = .Id
            TextBoxPais.Text = BLL_Atlas.Nom(.Country, BLL.BLLSession.Current.Lang)
            TextBoxBankNom.Text = .RaoSocial
            TextBoxBankAlias.Text = .NomComercial
            TextBoxBankTel.Text = .Tel
            If .Swift IsNot Nothing Then
                TextBoxBankSwift.Text = .Swift
            End If
            TextBoxBankWeb.Text = .Web
            PictureBoxBrowse.Enabled = (TextBoxBankWeb.Text > "")
            Xl_ImageLogo.Bitmap = .Logo
            CheckBoxObsoleto.Checked = .Obsoleto
            CheckBoxSEPAB2B.Checked = .SEPAB2B
            ButtonDel.Enabled = Not .isnew
        End With

    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With _Bank
            .Id = TextBoxBankCod.Text
            .RaoSocial = TextBoxBankNom.Text
            .NomComercial = TextBoxBankAlias.Text
            .Tel = TextBoxBankTel.Text
            .Swift = TextBoxBankSwift.Text
            .Web = TextBoxBankWeb.Text
            .Logo = Xl_ImageLogo.Bitmap
            .SEPAB2B = CheckBoxSEPAB2B.Checked
            .Obsoleto = CheckBoxObsoleto.Checked
        End With

        Dim exs as New List(Of exception)
        If BLL.BLLBank.update(_Bank, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Bank))
            Me.Close()
        Else
            UIHelper.WarnError(exs, "error al desar la entitat bancària")
        End If
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        Select Case TabControl1.SelectedIndex
            Case Tabs.Agcs
                Static LoadedAgcs As Boolean
                If _AllowEvents And Not LoadedAgcs Then
                    LoadBranches()
                    LoadedAgcs = True
                End If
            Case Tabs.Group
                Static LoadedGroup As Boolean
                If _AllowEvents And Not LoadedGroup Then
                    Dim oBankGroup As DTOBankGroup = BLL.BLLBankGroup.FromSameBank(_Bank)
                    Xl_Banks1.Load(oBankGroup.Banks, BLL.Defaults.SelectionModes.Browse)
                    LoadedGroup = True
                End If
            Case Tabs.Ibans
                Static LoadedCtas As Boolean
                If _AllowEvents And Not LoadedCtas Then
                    Dim oIbans As List(Of DTOIban) = BLL.BLLIbans.FromBank(_Bank)
                    Xl_Ibans1.Load(oIbans)
                    LoadedCtas = True
                End If
        End Select
    End Sub


    Private Sub TextBox_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        TextBoxBankCod.TextChanged, _
        TextBoxBankSwift.TextChanged, _
        CheckBoxObsoleto.CheckedChanged, _
        TextBoxBankTel.TextChanged, _
        TextBoxBankAlias.TextChanged, _
        TextBoxBankNom.TextChanged, _
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


    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("eliminem el banc" & vbCrLf & BLL.BLLBank.NomComercialORaoSocial(_Bank) & "?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of exception)
            If BLL.BLLBank.delete(_Bank, exs) Then
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
            Dim BlValidUrl As Boolean = MaxiSrvr.IsValidUrl(TextBoxBankWeb.Text)
            PictureBoxBrowse.Enabled = BlValidUrl
            SetDirty()
        End If
    End Sub

    Private Sub PictureBoxBrowse_Click(sender As Object, e As System.EventArgs) Handles PictureBoxBrowse.Click
        UIHelper.ShowHtml(TextBoxBankWeb.Text)
    End Sub

    Private Sub Xl_BankBranches1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_BankBranches1.RequestToAddNew
        Dim oBranch As DTOBankBranch = BLL.BLLBankBranch.NewBranch(Xl_Banks1.Value)
        Dim oFrm As New Frm_BankBranch(oBranch)
        AddHandler oFrm.AfterUpdate, AddressOf LoadBranches
        oFrm.Show()
    End Sub

    Private Sub LoadBranches()
        Dim oBranches As List(Of DTOBankBranch) = BLL.BLLBankBranches.FromBank(_Bank)
        Xl_BankBranches1.Load(oBranches, BLL.Defaults.SelectionModes.Browse)
    End Sub
End Class