Public Class Frm_BankBranch

    Private _Branch As DTOBankBranch
    Private _Structure As DTOIbanStructure
    Private _Loaded As Boolean
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Private Enum Tabs
        Gral
        Bank
        Ibans
    End Enum

    Public Sub New(ByVal oBranch As DTOBankBranch)
        MyBase.New()
        Me.InitializeComponent()
        _Branch = oBranch
    End Sub

    Private Async Sub Frm_BankBranch_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB2.BankBranch.Load(_Branch, exs) Then
            Await refresca()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        If _Branch.Bank Is Nothing Then
            If TextBoxCod.Text.Length >= 2 Then
                _Structure = Await FEB2.IbanStructure.Find(TextBoxCod.Text.Substring(0, 2), exs)
            End If
        Else
            If _Branch.Bank.Country IsNot Nothing Then
                _Structure = Await FEB2.IbanStructure.Find(_Branch.Bank.Country, exs)
            End If
        End If
        If _Structure IsNot Nothing Then TextBoxCod.MaxLength = _Structure.BranchLength
        If _Branch.Bank IsNot Nothing Then
            With _Branch
                With .Bank
                    Dim s As String = .Id
                    If .NomComercial > "" Then s = s & vbCrLf & .NomComercial
                    s = s & vbCrLf & .RaoSocial
                    TextBoxBank.Text = s
                    PictureBoxBankLogo.Image = LegacyHelper.ImageHelper.Converter(.logo)

                    TextBoxBankSwift.Text = .swift
                    Xl_ImageLogo.Bitmap = LegacyHelper.ImageHelper.Converter(.logo)
                    If .Country IsNot Nothing Then
                        TextBoxPais.Text = .Country.LangNom.Esp
                    End If
                    TextBoxBankCod.Text = .Id
                    TextBoxBankNom.Text = .RaoSocial
                    TextBoxBankAlias.Text = .NomComercial
                    TextBoxBankTel.Text = .Tel
                    TextBoxBankWeb.Text = .Web
                End With
                If IsNumeric(.Id) Then
                    If Val(.Id) > 0 Then
                        TextBoxCod.Text = .Id
                    End If
                End If
                TextBoxAdr.Text = .Address
                Xl_LookupLocation1.LocationValue = .Location
                TextBoxTel.Text = .Tel
                ButtonDel.Enabled = Not .IsNew
            End With
        End If
        _AllowEvents = True
    End Function


    Private Async Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With _Branch
            .Id = TextBoxCod.Text
            .Address = TextBoxAdr.Text
            .Location = Xl_LookupLocation1.LocationValue
            .Tel = TextBoxTel.Text
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        If Await FEB2.BankBranch.Update(_Branch, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Branch))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs, "error al desar la oficina bancària")
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControlBank.SelectedIndexChanged
        Dim exs As New List(Of Exception)
        Select Case TabControlBank.SelectedIndex
            Case Tabs.Ibans
                If _AllowEvents And Not _Loaded Then
                    Dim oIbans = Await FEB2.Ibans.FromBankBranch(exs, Current.Session.Emp, _Branch)
                    If exs.Count = 0 Then
                        Xl_Ibans1.Load(oIbans)
                    Else
                        UIHelper.WarnError(exs)
                    End If
                    _Loaded = True
                End If
        End Select
    End Sub



    Private Async Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("eliminem aquesta oficina?" & vbCrLf & _Branch.Address & "?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB2.BankBranch.Delete(_Branch, exs) Then
                MsgBox("oficina bancària eliminada", MsgBoxStyle.Information, "MAT.NET")
                RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar la oficina bancària")
            End If
        Else
            MsgBox("Operacio cancelada per l'usuari", MsgBoxStyle.Exclamation, "MAT.NET")
        End If
    End Sub

    Private Async Sub TextBoxCod_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        TextBoxCod.TextChanged
        If _AllowEvents Then
            If _Structure IsNot Nothing Then
                Dim sId As String = TextBoxCod.Text
                If sId.Length = _Structure.BranchLength Then
                    Dim exs As New List(Of Exception)
                    Dim oAltBranch = Await FEB2.BankBranch.Find(_Branch.Bank, sId, exs)
                    If exs.Count = 0 Then
                        If oAltBranch IsNot Nothing Then
                            _Branch = oAltBranch
                            PictureBoxId.Image = My.Resources.vb
                            Await refresca()
                        End If
                        PictureBoxId.Image = My.Resources.vb
                    Else
                        UIHelper.WarnError(exs)
                    End If
                Else
                    PictureBoxId.Image = My.Resources.warn
                End If
            End If
            SetDirty()
        End If
    End Sub

    Private Sub TextBoxAdr_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        TextBoxAdr.TextChanged, TextBoxBankTel.TextChanged
        If _AllowEvents Then
            SetDirty()
        End If
    End Sub


    Private Sub SetDirty()
        ButtonOk.Enabled = (TextBoxCod.Text.isNotEmpty())
    End Sub

    Private Sub Xl_LookupLocation1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_LookupLocation1.AfterUpdate
        _Branch.Location = e.Argument
        'refresca()
        SetDirty()
    End Sub


End Class
