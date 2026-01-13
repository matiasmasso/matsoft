

Public Class Xl_Regio
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
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ContextMenu1 As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItemZoom As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSelectNew As System.Windows.Forms.MenuItem
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label
        Me.ContextMenu1 = New System.Windows.Forms.ContextMenu
        Me.MenuItemZoom = New System.Windows.Forms.MenuItem
        Me.MenuItemSelectNew = New System.Windows.Forms.MenuItem
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label1.ContextMenu = Me.ContextMenu1
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(128, 20)
        Me.Label1.TabIndex = 2
        '
        'ContextMenu1
        '
        Me.ContextMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemZoom, Me.MenuItemSelectNew})
        '
        'MenuItemZoom
        '
        Me.MenuItemZoom.Index = 0
        Me.MenuItemZoom.Text = "Zoom"
        '
        'MenuItemSelectNew
        '
        Me.MenuItemSelectNew.Index = 1
        Me.MenuItemSelectNew.Text = "Cambiar..."
        '
        'Xl_Regio
        '
        Me.Controls.Add(Me.Label1)
        Me.Name = "Xl_Regio"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Event AfterUpdate(ByVal oRegio As Regio)

    Private mRegio As Regio
    Private mLocked As Boolean

    Private COLOR_UNLOCKED As System.Drawing.Color = System.Drawing.Color.White
    Private COLOR_LOCKED As System.Drawing.Color = System.Drawing.Color.LightGray

    Public Property Regio() As Regio
        Get
            Return mRegio
        End Get
        Set(ByVal Value As Regio)
            mRegio = Value
            DisplayRegio()
        End Set
    End Property

    Public Property Locked() As Boolean
        Get
            Return mLocked
        End Get
        Set(ByVal Value As Boolean)
            mLocked = Value
            MenuItemSelectNew.Enabled = Not mLocked
            Label1.BackColor = IIf(mLocked, COLOR_LOCKED, COLOR_UNLOCKED)
        End Set
    End Property

    Private Sub Xl_Regio_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        MyBase.Height = Label1.Height
        Label1.Width = MyBase.Width
    End Sub

    Private Sub MenuItemSelectNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSelectNew.Click
        SelectNew()
    End Sub

    Private Sub MenuItemZoom_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemZoom.Click
        Zoom()
    End Sub

    Private Sub Zoom()
        root.ShowRegio(mRegio)
        DisplayRegio()
    End Sub

    Private Sub Label1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Label1.DoubleClick
        If mLocked Then
            Zoom()
        Else
            SelectNew()
        End If
    End Sub

    Private Sub SelectNew()
        Me.Regio = root.SelectRegioFromPais(mRegio.Country, mRegio)
        RaiseEvent AfterUpdate(mRegio)
    End Sub

    Private Sub DisplayRegio()
        If Not mRegio Is Nothing Then
            Label1.Text = mRegio.Nom
        End If
    End Sub
End Class