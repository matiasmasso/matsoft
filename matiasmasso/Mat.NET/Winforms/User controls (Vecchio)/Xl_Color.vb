Imports System.Drawing

Public Class Xl_Color
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
    Friend WithEvents MenuItemSwitch As System.Windows.Forms.MenuItem
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label
        Me.ContextMenu1 = New System.Windows.Forms.ContextMenu
        Me.MenuItemSwitch = New System.Windows.Forms.MenuItem
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.LightCyan
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label1.ContextMenu = Me.ContextMenu1
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(150, 130)
        Me.Label1.TabIndex = 0
        '
        'ContextMenu1
        '
        Me.ContextMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemSwitch})
        '
        'MenuItemSwitch
        '
        Me.MenuItemSwitch.Index = 0
        Me.MenuItemSwitch.Text = "Canviar"
        '
        'Xl_Color
        '
        Me.Controls.Add(Me.Label1)
        Me.Name = "Xl_Color"
        Me.Size = New System.Drawing.Size(150, 130)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Event AfterUpdate(ByVal oColor As Color)

    Public Property Color() As Color
        Get
            Return Label1.BackColor
        End Get
        Set(ByVal Value As Color)
            Label1.BackColor = Value
        End Set
    End Property

    Private Sub MenuItemSwitch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MenuItemSwitch.Click
        Dim oColorDialog As New System.Windows.Forms.ColorDialog
        With oColorDialog
            ' Keeps the user from selecting a custom color.
            '.AllowFullOpen = False
            .AllowFullOpen = True
            ' Allows the user to get help. (The default is false.)
            .ShowHelp = True
            ' Sets the initial color select to the current text color,
            .Color = Label1.BackColor

            ' Update the text box color if the user clicks OK 
            If (oColorDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK) Then
                Label1.BackColor = oColorDialog.Color
                RaiseEvent AfterUpdate(Label1.BackColor)
            End If
        End With
    End Sub
End Class
