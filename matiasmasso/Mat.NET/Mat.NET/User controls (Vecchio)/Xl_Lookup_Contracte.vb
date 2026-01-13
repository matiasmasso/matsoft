

Public Class Xl_Lookup_Contracte
    Inherits Xl_LookupTextboxButton

    Private mContract As Contract

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Property Contract() As Contract
        Get
            Return mContract
        End Get
        Set(ByVal value As Contract)
            mContract = value
            If mContract Is Nothing Then
                MyBase.Text = ""
            Else
                MyBase.Text = mContract.FullText
            End If
        End Set
    End Property

    Public Sub Clear()
        Me.Contract = Nothing
    End Sub

    Private Sub Xl_Lookup_Contracte_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.DoubleClick
        Dim oFrm As New Frm_Contract(mContract)
        AddHandler oFrm.AfterUpdate, AddressOf refreshrequest
        oFrm.Show()
    End Sub

    Private Sub Xl_LookupContract_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        Dim oFrm As New Frm_Contracts(bll.dEFAULTS.SelectionModes.Selection, mContract)
        AddHandler oFrm.AfterSelect, AddressOf onContractSelected
        oFrm.Show()
    End Sub

    Private Sub onContractSelected(ByVal sender As Object, ByVal e As System.EventArgs)
        If TypeOf (sender) Is Contract Then
            mContract = sender
            MyBase.Text = mContract.FullText
            RaiseEvent AfterUpdate(sender, EventArgs.Empty)
        End If
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        MyBase.Text = mContract.FullText
    End Sub
End Class
