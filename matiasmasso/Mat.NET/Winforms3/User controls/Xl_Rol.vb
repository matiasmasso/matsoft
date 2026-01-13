

Public Class Xl_Rol
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
    Friend WithEvents ContextMenu1 As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItemZoom As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemChange As System.Windows.Forms.MenuItem
    Friend WithEvents Label1 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.ContextMenu1 = New System.Windows.Forms.ContextMenu
        Me.MenuItemZoom = New System.Windows.Forms.MenuItem
        Me.MenuItemChange = New System.Windows.Forms.MenuItem
        Me.Label1 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'ContextMenu1
        '
        Me.ContextMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemZoom, Me.MenuItemChange})
        '
        'MenuItemZoom
        '
        Me.MenuItemZoom.Index = 0
        Me.MenuItemZoom.Text = "Propietats"
        '
        'MenuItemChange
        '
        Me.MenuItemChange.Index = 1
        Me.MenuItemChange.Text = "canviar..."
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label1.ContextMenu = Me.ContextMenu1
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(112, 20)
        Me.Label1.TabIndex = 0
        '
        'Xl_Rol
        '
        Me.Controls.Add(Me.Label1)
        Me.Name = "Xl_Rol"
        Me.Size = New System.Drawing.Size(150, 112)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Private mRol As DTORol

    Public Property Rol() As DTORol
        Get
            Return mRol
        End Get
        Set(ByVal Value As DTORol)
            mRol = Value
            If mRol Is Nothing Then
                Label1.Text = ""
            Else
                If mRol.Nom Is Nothing Then
                    Label1.Text = mRol.Id
                Else
                    Label1.Text = mRol.Nom.Tradueix(DTOApp.current.lang)
                End If
                Dim DisableChange As Boolean = True
                Select Case Current.Session.User.Rol.id
                    Case DTORol.Ids.SuperUser, DTORol.Ids.Admin
                        Select Case mRol.Id
                            Case DTORol.Ids.SuperUser, DTORol.Ids.Admin
                            Case Else
                                DisableChange = False
                        End Select

                    Case DTORol.Ids.Accounts, DTORol.Ids.LogisticManager, DTORol.Ids.Operadora, DTORol.Ids.Comercial, DTORol.Ids.Marketing
                        Select Case mRol.Id
                            Case DTORol.Ids.Guest, DTORol.Ids.Comercial, DTORol.Ids.NotSet, DTORol.Ids.CliFull, DTORol.Ids.CliLite, DTORol.Ids.Lead, DTORol.Ids.Unregistered
                                DisableChange = False
                        End Select

                End Select
                MenuItemChange.Enabled = Not DisableChange
            End If
        End Set
    End Property

    Private Sub Xl_Rol_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        Label1.Width = MyBase.Width
        MyBase.Height = Label1.Height
    End Sub


    Private Sub MenuItemZoom_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemZoom.Click
        Zoom()
    End Sub

    Private Sub MenuItemChange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemChange.Click
        Dim oFrm As New Frm_Rol_Select(mRol, DTO.Defaults.SelectionModes.Selection)
        AddHandler oFrm.itemSelected, AddressOf onRolSelected
        oFrm.Show()
    End Sub

    Private Sub onRolSelected(sender As Object, e As MatEventArgs)
        mRol = e.Argument
        If mRol.Nom Is Nothing Then
            Label1.Text = mRol.Id
        Else
            Label1.Text = mRol.Nom.Tradueix(DTOApp.current.lang)
        End If
        RaiseEvent AfterUpdate(Me, New MatEventArgs(mRol))
    End Sub

    Private Sub Zoom()
        Dim oFrm As New Frm_Rol(mRol)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Sub refresca(sender As Object, e As MatEventArgs)
        Label1.Text = mRol.Nom.Tradueix(Current.Session.User.Lang)
    End Sub

    Private Sub Label1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.DoubleClick
        Zoom()
    End Sub
End Class
