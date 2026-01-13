Public Class Frm_Albs

    Public Sub New(oAlbs As Albs, sTitle As String)
        MyBase.New()
        Me.InitializeComponent()
        Me.Text = sTitle

        Xl_Albs1.Load(oAlbs)
    End Sub
End Class