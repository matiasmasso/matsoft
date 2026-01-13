

Public Class Xl_Contact_Trp
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

    Friend WithEvents Label2 As Label
    Friend WithEvents TextBoxCubicatje As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBoxNom As TextBox

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxCubicatje = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxNom = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(25, 50)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 16)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "cubicatje:"
        '
        'TextBoxCubicatje
        '
        Me.TextBoxCubicatje.Location = New System.Drawing.Point(89, 50)
        Me.TextBoxCubicatje.Name = "TextBoxCubicatje"
        Me.TextBoxCubicatje.Size = New System.Drawing.Size(56, 20)
        Me.TextBoxCubicatje.TabIndex = 7
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(25, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 16)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "nom:"
        '
        'TextBoxNom
        '
        Me.TextBoxNom.Location = New System.Drawing.Point(89, 26)
        Me.TextBoxNom.Name = "TextBoxNom"
        Me.TextBoxNom.Size = New System.Drawing.Size(258, 20)
        Me.TextBoxNom.TabIndex = 5
        '
        'Xl_Contact_Trp
        '
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBoxCubicatje)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBoxNom)
        Me.Name = "Xl_Contact_Trp"
        Me.Size = New System.Drawing.Size(376, 272)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Public Event AfterUpdate()
    Public Event UpdateChanges()

    Private mDirty As Boolean
    Private mTrp As DTOTransportista
    Private mAllowEvents As Boolean

    Public ReadOnly Property Transportista() As DTOTransportista
        Get
            With mTrp
                .Abr = TextBoxNom.Text
                .Cubicaje = If(IsNumeric(TextBoxCubicatje.Text), TextBoxCubicatje.Text, 0)
            End With
            Return mTrp
        End Get
    End Property

    Public Shadows Sub Load(value As DTOTransportista)
        mTrp = value
        With mTrp
            TextBoxNom.Text = .Abr
            TextBoxCubicatje.Text = IIf(.Cubicaje = 0, "", .Cubicaje)
        End With
        mAllowEvents = True
    End Sub


    Private Sub ControlValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxNom.TextChanged, TextBoxCubicatje.TextChanged

        If mAllowEvents Then
            SetDirty()
        End If
    End Sub

    Private Sub SetDirty()
        mDirty = True
        RaiseEvent AfterUpdate()
    End Sub

End Class
