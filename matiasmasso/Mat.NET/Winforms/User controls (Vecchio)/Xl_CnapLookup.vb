

Public Class Xl_CnapLookup
    Inherits Xl_LookupTextboxButton

    Private mCnap As DTOCnap

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Property Cnap() As DTOCnap
        Get
            Return mCnap
        End Get
        Set(ByVal value As DTOCnap)
            mCnap = value
            If mCnap Is Nothing Then
                MyBase.Text = ""
            Else
                BLLCnap.Load(mCnap)
                MyBase.Text = BLLCnap.FullNom(mCnap, BLLSession.Current.Lang)
            End If
        End Set
    End Property

    Public Sub Clear()
        Me.Cnap = Nothing
    End Sub

    Private Sub Xl_LookupCnap_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        Dim oFrm As New Frm_Cnaps(bll.dEFAULTS.SelectionModes.Selection, mCnap)
        AddHandler oFrm.AfterSelect, AddressOf onCnapSelected
        oFrm.Show()
    End Sub

    Private Sub onCnapSelected(ByVal sender As Object, ByVal e As MatEventArgs)
        mCnap = e.Argument
        If mCnap IsNot Nothing Then
            MyBase.Text = BLLCnap.FullNom(mCnap, BLLSession.Current.Lang)
            RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
        End If
    End Sub



End Class
