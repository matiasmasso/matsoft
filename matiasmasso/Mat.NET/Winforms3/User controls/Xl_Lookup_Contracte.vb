

Public Class Xl_Lookup_Contracte
    Inherits Xl_LookupTextboxButton

    Private _Contract As DTOContract

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Property Contract() As DTOContract
        Get
            Return _Contract
        End Get
        Set(ByVal value As DTOContract)
            _Contract = value
            If _Contract Is Nothing Then
                MyBase.Text = ""
            Else
                MyBase.Text = DTOContract.FullText(_Contract)
            End If
        End Set
    End Property

    Public Sub Clear()
        Me.Contract = Nothing
    End Sub

    Private Sub Xl_Lookup_Contracte_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Doubleclick
        Dim oFrm As New Frm_Contract(_Contract)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Xl_LookupContract_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        Dim oFrm As New Frm_Contracts(_Contract, DTO.Defaults.SelectionModes.Selection)
        AddHandler oFrm.ItemSelected, AddressOf onContractSelected
        oFrm.Show()
    End Sub

    Private Sub onContractSelected(ByVal sender As Object, ByVal e As MatEventArgs)
        If TypeOf (e.Argument) Is DTOContract Then
            _Contract = e.Argument
            MyBase.Text = DTOContract.FullText(_Contract)
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Contract))
        End If
    End Sub

    Private Sub RefreshRequest()
        MyBase.Text = DTOContract.FullText(_Contract)
    End Sub
End Class
