
Public Class Frm_PndsToCcb
    Private mContact As Contact

    Public Sub New(ByVal oContact As Contact)
        MyBase.New()
        Me.InitializeComponent()
        mContact = oContact
        LoadGrid()
    End Sub

    Private Sub LoadGrid()
        Dim SQL As String = ""
    End Sub
End Class