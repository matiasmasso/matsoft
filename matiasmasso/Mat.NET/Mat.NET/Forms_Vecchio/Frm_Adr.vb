

Public Class Frm_Adr
    Private mAdr As Adr = Nothing
    Private mAdrChanged As Boolean
    Private mCitChanged As Boolean
    Private mAllowEvents As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event AfterUpdateAdr(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event AfterUpdateCit(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oAdr As Adr)
        MyBase.New()
        Me.InitializeComponent()
        If oAdr IsNot Nothing Then
            mAdr = oAdr
            refresca()
            mAllowEvents = True
        End If
    End Sub

    Private Sub refresca()
        TextBoxAdr.Text = mAdr.Text
        Xl_Zip1.Zip = mAdr.Zip
    End Sub

    Public ReadOnly Property Adr() As Adr
        Get
            Return mAdr
        End Get
    End Property

    Private Sub TextBoxAdr_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxAdr.TextChanged
        If mAllowEvents Then
            mAdrChanged = True
            ButtonOk.Enabled = True
        End If

    End Sub

    Private Sub Xl_Cit1_AfterUpdate(sender As Object, e As System.EventArgs) Handles Xl_Zip1.AfterUpdate
        If mAllowEvents Then
            mCitChanged = True
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        If mAdr Is Nothing Then mAdr = New Adr
        With mAdr
            .Text = TextBoxAdr.Text
            .Zip = Xl_Zip1.Zip
        End With
        If mAdrChanged Then
            RaiseEvent AfterUpdateAdr(mAdr, EventArgs.Empty)
        End If
        If mCitChanged Then
            RaiseEvent AfterUpdateCit(mAdr, EventArgs.Empty)
        End If
        RaiseEvent AfterUpdate(mAdr, EventArgs.Empty)
        Me.Close()
    End Sub
End Class