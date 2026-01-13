

Public Class Xl_Mgzs_ComboBox
    Inherits System.Windows.Forms.UserControl

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'UserControl overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox
        Me.SuspendLayout()
        '
        'ComboBox1
        '
        Me.ComboBox1.Location = New System.Drawing.Point(0, 0)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(72, 21)
        Me.ComboBox1.TabIndex = 0
        '
        'Xl_Mgzs_ComboBox
        '
        Me.Controls.Add(Me.ComboBox1)
        Me.Name = "Xl_Mgzs_ComboBox"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Private mMgzs As List(Of DTOMgz)
    Private mAllowEvents As Boolean

    Public WriteOnly Property Mgzs() As List(Of DTOMgz)
        Set(ByVal Value As List(Of DTOMgz))
            mMgzs = Value
            LoadMgzs()
            ComboBox1.SelectedIndex = 0
            mAllowEvents = True
        End Set
    End Property

    Public Property Mgz() As DTOMgz
        Get
            Return CurrentMgz()
        End Get
        Set(ByVal Value As DTOMgz)
            If Not Value Is Nothing Then
                Dim BlFound As Boolean
                mAllowEvents = False
                If mMgzs Is Nothing Then
                    Dim oMgzs As List(Of DTOMgz) = BLLMgzs.Actius(Current.Session.Emp)
                    Me.Mgzs = oMgzs
                End If
                Dim i As Integer
                For i = 0 To mMgzs.Count - 1
                    If mMgzs(i).Equals(Value) Then
                        ComboBox1.SelectedIndex = i
                        BlFound = True
                        Exit For
                    End If
                Next
                If Not BlFound And mMgzs.Count > 0 Then
                    ComboBox1.SelectedIndex = 0
                End If
                mAllowEvents = True
            End If
        End Set
    End Property

    Private Function CurrentMgz() As DTOMgz
        Dim oMgz As DTOMgz = Nothing
        If ComboBox1.SelectedIndex >= 0 Then
            oMgz = mMgzs(ComboBox1.SelectedIndex)
        End If
        Return oMgz
    End Function

    Private Sub LoadMgzs()
        Dim oMgz As DTOMgz
        ComboBox1.Items.Clear()

        For Each oMgz In mMgzs
            ComboBox1.Items.Add(DTOMgz.AbrOrNom(oMgz))
        Next
    End Sub

    Private Sub Xl_Mgzs_ComboBox_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        ComboBox1.Width = MyBase.Width
        MyBase.Height = ComboBox1.Height
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        If mallowevents Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(CurrentMgz))
        End If
    End Sub
End Class
