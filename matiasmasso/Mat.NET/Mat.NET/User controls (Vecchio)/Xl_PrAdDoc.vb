Public Class Xl_PrAdDoc

    Private mAdDoc As PrAdDoc = Nothing
    Private mRevista As PrRevista = Nothing

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public WriteOnly Property Revista() As PrRevista
        Set(ByVal value As PrRevista)
            mRevista = value
        End Set
    End Property

    Public Property AdDoc() As PrAdDoc
        Get
            Return mAdDoc
        End Get
        Set(ByVal value As PrAdDoc)
            If value IsNot Nothing Then
                mAdDoc = value
                TextBox1.Text = mAdDoc.FullText
            End If
        End Set
    End Property

    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim oFrm As New Frm_PrAdDocs(bll.dEFAULTS.SelectionModes.Selection) 'mAdDoc, mRevista)
        AddHandler oFrm.AfterSelect, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As MatEventArgs)
        mAdDoc = e.Argument
        TextBox1.Text = mAdDoc.FullText
        Dim oArgs As New MatEventArgs(mAdDoc)
        RaiseEvent AfterUpdate(Me, oArgs)
    End Sub
End Class
