

Public Class Xl_Private

    Private mUsr As Contact = root.Usuari
    Private mRolId As DTORol.Ids = mUsr.Rol.Id
    Private mAllowBrowse As Boolean = True
    Private mLocked As Boolean

    Private Enum ProtectionLevels
        None
        Low
        Mid
        High
        Max
    End Enum

    Public WriteOnly Property Contact() As Contact
        Set(ByVal value As Contact)
            mAllowBrowse = True
            Dim oLevel As ProtectionLevels = ProtectionLevel(value)
            mLocked = (oLevel > ProtectionLevels.Mid)
            PictureBox1.Visible = mLocked
            If mUsr.Id <> value.Id Then
                Select Case mRolId
                    Case DTORol.Ids.SuperUser, DTORol.Ids.Admin
                    Case Else
                        If oLevel > ProtectionLevels.Mid Then
                            mAllowBrowse = False
                        End If
                End Select
            End If
        End Set
    End Property

    Public ReadOnly Property AllowBrowse() As Boolean
        Get
            Return mAllowBrowse
        End Get
    End Property

    Public ReadOnly Property Locked() As Boolean
        Get
            Return mLocked
        End Get
    End Property

    Private Function ProtectionLevel(ByVal oContact As Contact) As ProtectionLevels
        Dim retval As ProtectionLevels = ProtectionLevels.None
        Select Case oContact.Rol.Id
            Case DTORol.Ids.SuperUser, DTORol.Ids.Admin
                retval = ProtectionLevels.Max
            Case DTORol.Ids.SalesManager, DTORol.Ids.Comercial, DTORol.Ids.Accounts, Rol.Ids.LogisticManager, DTORol.Ids.Operadora, DTORol.Ids.Marketing
                retval = ProtectionLevels.High
            Case DTORol.Ids.Rep
                retval = ProtectionLevels.Mid
            Case DTORol.Ids.Cli
                retval = ProtectionLevels.Low
        End Select
        Return retval
    End Function

    Private Sub PictureBox1_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox1.Resize
        Me.Height = PictureBox1.Height
        Me.Width = PictureBox1.Width
    End Sub
End Class
