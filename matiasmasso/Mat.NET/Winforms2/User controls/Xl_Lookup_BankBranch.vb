Public Class Xl_Lookup_BankBranch
    Inherits Xl_LookupTextboxButton

    Private _BankBranch As DTOBankBranch
    Private _Iban As DTOIban
    Private _Bank As DTOBank
    Private _Digits As String

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)



    Public Shadows Async Function Load(oIban As DTOIban) As Task
        Dim exs As New List(Of Exception)
        _Iban = oIban

        If oIban.BankBranch Is Nothing Then
            oIban.BankBranch = Await FEB.IbanStructure.GetBankBranch(oIban.Digits, exs)
            RaiseEvent AfterUpdate(Me, New MatEventArgs(oIban))
        End If

        If oIban.BankBranch Is Nothing Then
            _Bank = DTOIban.Bank(oIban)
            If _Bank Is Nothing Then
                MyBase.BackColor = Color.LightSalmon
                MyBase.Text = "(clic al botó per donar d'alta el banc)"
            Else
                MyBase.BackColor = Color.LightYellow
                MyBase.Text = DTOBank.NomComercialORaoSocial(_Bank) & " (clic al botó per donar d'alta la oficina)"
            End If
        Else
            _BankBranch = oIban.BankBranch
            Me.Branch = _BankBranch
        End If
        MyBase.TextBox1.ReadOnly = True
        SetContextMenu()
    End Function

    Public Shadows Async Function Load(sDigits As String) As Task
        Dim exs As New List(Of Exception)
        _Digits = sDigits
        _Iban = DTOIban.Factory(_Digits)
        _BankBranch = Await FEB.IbanStructure.GetBankBranch(_Digits, exs)
        If exs.Count = 0 Then
            If _BankBranch Is Nothing Then
                _Bank = Await FEB.IbanStructure.GetBank(_Digits, exs)
                If _Bank Is Nothing Then
                    MyBase.BackColor = Color.LightSalmon
                    MyBase.Text = "(clic al botó per donar d'alta el banc)"
                Else
                    MyBase.BackColor = Color.LightYellow
                    MyBase.Text = DTOBank.NomComercialORaoSocial(_Bank) & " (clic al botó per donar d'alta la oficina)"
                End If
            Else
                Me.Branch = _BankBranch
            End If
            MyBase.TextBox1.ReadOnly = True
            SetContextMenu()
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        If _BankBranch Is Nothing Then
            If _Bank IsNot Nothing Then
                Dim oMenu_Bank As New Menu_Bank(_Bank)
                AddHandler oMenu_Bank.AfterUpdate, AddressOf RefreshRequest
                oContextMenu.Items.AddRange(oMenu_Bank.Range)
            End If
        Else
            Dim oMenu_BankBranch As New Menu_BankBranch(_BankBranch)
            AddHandler oMenu_BankBranch.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_BankBranch.Range)
        End If
        TextBox1.ContextMenuStrip = oContextMenu
    End Sub

    Public Property Branch As DTOBankBranch
        Get
            Return MyBase.Value
        End Get
        Set(value As DTOBankBranch)
            MyBase.Value = value
            If value Is Nothing Then
                MyBase.Text = ""
            Else
                MyBase.Text = DTOBankBranch.FullNomAndAddress(value)
            End If
        End Set
    End Property

    Private Sub Xl_Lookup_BankBranch_Doubleclick(sender As Object, e As EventArgs) Handles Me.Doubleclick
        If _BankBranch IsNot Nothing Then
            Dim oFrm As New Frm_BankBranch(_BankBranch)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        ElseIf _Bank IsNot Nothing Then
            Dim oFrm As New Frm_Bank(_Bank)
            AddHandler oFrm.AfterUpdate, AddressOf onBankUpdate
            oFrm.Show()
        End If
    End Sub

    Private Async Sub Xl_Country_onLookUpRequest(sender As Object, e As EventArgs) Handles Me.onLookUpRequest
        Dim exs As New List(Of Exception)
        If _BankBranch Is Nothing Then
            If _Bank Is Nothing Then
                _Bank = Await FEB.Iban.GetBankFromDigits(_Iban, exs)
            End If
            If _Bank Is Nothing Then
                Dim oCountry = Await FEB.Iban.Country(_Iban, exs)
                _Bank = DTOBank.Factory(oCountry, FEB.Iban.BankId(_Iban))
                Dim oFrm As New Frm_Bank(_Bank)
                AddHandler oFrm.AfterUpdate, AddressOf onBankUpdate
                oFrm.Show()
            Else
                Dim oBranch As DTOBankBranch = DTOBankBranch.Factory(_Bank)
                oBranch.Id = DTOIban.BranchId(_Iban)
                Dim oFrm As New Frm_BankBranch(oBranch)
                AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                oFrm.Show()
            End If
        Else
            Dim oFrm As New Frm_Banks(, Frm_Banks.Modes.SelectBranch)
            AddHandler oFrm.OnItemSelected, AddressOf RefreshRequest
            oFrm.Show()
        End If
    End Sub

    Private Sub onBankUpdate(sender As Object, e As MatEventArgs)
        Dim oBank As DTOBank = e.Argument
        MyBase.Text = DTOBank.NomComercialORaoSocial(oBank) & " (clic al botó per donar d'alta la oficina)"
        MyBase.BackColor = Color.LightYellow
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As MatEventArgs)
        Dim oBranch As DTOBankBranch = e.Argument
        Me.Branch = oBranch
        MyBase.IsDirty = True
        If Not oBranch.Equals(_BankBranch) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_BankBranch))
        End If
    End Sub
End Class
