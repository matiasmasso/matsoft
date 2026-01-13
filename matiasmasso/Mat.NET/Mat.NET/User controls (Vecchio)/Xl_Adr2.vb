

Public Class Xl_Adr2
    Inherits Xl_LookupTextboxButton

    Private mAdr As Adr

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Property Adr() As Adr
        Get
            Return mAdr
        End Get
        Set(ByVal value As Adr)
            mAdr = value
            If mAdr IsNot Nothing Then
                MyBase.Text = mAdr.FullText
            End If
        End Set
    End Property

    Private Sub Xl_Adr2_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        Dim oAdr As Adr = Nothing
        If mAdr IsNot Nothing Then
            oAdr = mAdr.Clon
        End If
        Dim oFrm As New Frm_Adr(oAdr)
        AddHandler oFrm.AfterUpdateAdr, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        If Not sender.Equals(mAdr) Then
            Me.Adr = sender
            MyBase.IsDirty = True
            RaiseEvent AfterUpdate(mAdr, EventArgs.Empty)
        End If
    End Sub

    Public Function UpdateIfDirty(ByRef oContact As Contact, ByVal oCod As Adr.Codis) As Boolean
        Dim retval As Boolean = False
        If MyBase.IsDirty Then
            mAdr.Update(oContact, oCod)
            retval = True
        End If
        Return retval
    End Function
End Class
