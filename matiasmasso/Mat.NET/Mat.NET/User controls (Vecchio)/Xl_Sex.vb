Public Class Xl_Sex
    Private mSex As Contact.Sexs = Contact.Sexs.NotSet
    Private mAllowEvents As Boolean = False

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As EventArgs)

    Public Property Sex() As Contact.Sexs
        Get
            Return mSex
        End Get
        Set(ByVal value As Contact.Sexs)
            mSex = value
            refresca()
            mAllowEvents = True
        End Set
    End Property

    Private Sub refresca()
        mAllowEvents = False
        ToolStripMenuItemMale.Checked = (mSex = Contact.Sexs.Male)
        ToolStripMenuItemFemale.Checked = (mSex = Contact.Sexs.Female)
        ToolStripMenuItemNotSet.Checked = (mSex = Contact.Sexs.NotSet)

        Select Case mSex
            Case Contact.Sexs.Male
                PictureBox1.Image = My.Resources.SexMale
            Case Contact.Sexs.Female
                PictureBox1.Image = My.Resources.SexFemale
            Case Else
                PictureBox1.Image = My.Resources.SexPending
        End Select
        mAllowEvents = True
    End Sub

    Private Sub PictureBox1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox1.DoubleClick
        Select Case mSex
            Case Contact.Sexs.Male
                mSex = Contact.Sexs.Female
            Case Contact.Sexs.Female
                mSex = Contact.Sexs.NotSet
            Case Else
                mSex = Contact.Sexs.Male
        End Select
        refresca()
        RaiseEvent AfterUpdate(mSex, EventArgs.Empty)
    End Sub

    Private Sub ToolStripMenuItem_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    ToolStripMenuItemMale.CheckedChanged, _
     ToolStripMenuItemFemale.CheckedChanged, _
      ToolStripMenuItemNotSet.CheckedChanged

        If mAllowEvents Then
            If ToolStripMenuItemMale.Checked Then
                mSex = Contact.Sexs.Male
            ElseIf ToolStripMenuItemFemale.Checked Then
                mSex = Contact.Sexs.Female
            Else
                mSex = Contact.Sexs.NotSet
            End If
            refresca()
            RaiseEvent AfterUpdate(mSex, EventArgs.Empty)
        End If
    End Sub
End Class
