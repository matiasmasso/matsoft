Public Class Xl_LookupBankBranch

    Inherits Xl_LookupTextboxButton

    Private _BankBranch As DTOBankBranch

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Shadows Property BankBranch() As DTOBankBranch
        Get
            Return _BankBranch
        End Get
        Set(ByVal value As DTOBankBranch)
            _BankBranch = value
            refresca()
        End Set
    End Property

    Public Sub Clear()
        Me.BankBranch = Nothing
    End Sub

    Private Sub Xl_LookupBankBranch_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        If _BankBranch IsNot Nothing Then
            Dim exs As New List(Of Exception)
            If Not FEB.BankBranch.Load(_BankBranch, exs) Then
                UIHelper.WarnError(exs)
                Exit Sub
            End If
        End If
        Dim oFrm As New Frm_Banks(_BankBranch, Frm_Banks.Modes.SelectBranch)
        AddHandler oFrm.onItemSelected, AddressOf onBankBranchSelected
        oFrm.Show()
    End Sub

    Private Sub onBankBranchSelected(ByVal sender As Object, ByVal e As MatEventArgs)
        _BankBranch = e.Argument
        refresca()
        RaiseEvent AfterUpdate(Me, e)
    End Sub

    Private Sub refresca()
        If _BankBranch Is Nothing Then
            MyBase.Text = ""
            MyBase.ClearContextMenu()
        Else
            MyBase.Text = DTOBankBranch.FullNomAndAddress(_BankBranch)
            Dim oMenu_BankBranch As New Menu_BankBranch(_BankBranch)
            AddHandler oMenu_BankBranch.AfterUpdate, AddressOf refresca
            MyBase.SetContextMenuRange(oMenu_BankBranch.Range)
        End If
    End Sub


End Class


