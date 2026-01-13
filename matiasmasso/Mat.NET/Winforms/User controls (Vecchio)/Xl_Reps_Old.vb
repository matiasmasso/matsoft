
Imports System.Data.SqlClient

Public Class Xl_Reps_Old
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
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.SuspendLayout()
        '
        'ComboBox1
        '
        Me.ComboBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ComboBox1.Location = New System.Drawing.Point(0, 0)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(192, 21)
        Me.ComboBox1.TabIndex = 0
        '
        'Xl_Reps_Old
        '
        Me.Controls.Add(Me.ComboBox1)
        Me.Name = "Xl_Reps_Old"
        Me.Size = New System.Drawing.Size(192, 24)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private mReps As List(Of Rep)
    Private mIncludeNoRep As Boolean = True
    Private mHideObsoletos As Boolean = False
    Private mAllowEvents As Boolean

    Public Event AfterUpdate(ByVal oRep As Rep)

    Public WriteOnly Property IncludeNoRep() As Boolean
        Set(ByVal Value As Boolean)
            mIncludeNoRep = Value
        End Set
    End Property

    Public WriteOnly Property HideObsoletos() As Boolean
        Set(ByVal Value As Boolean)
            mHideObsoletos = Value
        End Set
    End Property

    Public WriteOnly Property Reps() As List(Of Rep)
        Set(ByVal Value As List(Of Rep))
            If Not Value Is Nothing Then
                mReps = Value
                SetReps()
            End If
        End Set
    End Property

    Public ReadOnly Property Rep() As Rep
        Get
            Return CurrentRep()
        End Get
    End Property

    Private Sub SetReps()
        If mReps Is Nothing Then mReps = BLLApp.Emp.Reps(mHideObsoletos, mIncludeNoRep)
        Dim oRep As Rep
        For Each oRep In mReps
            ComboBox1.Items.Add(oRep.Abr)
        Next
        ComboBox1.SelectedIndex = 0
        mAllowEvents = True
    End Sub

    Private Sub Xl_Reps_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetReps()
    End Sub

    Private Function CurrentRep() As Rep
        Return mReps(ComboBox1.SelectedIndex)
    End Function

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        If mAllowEvents Then
            RaiseEvent AfterUpdate(CurrentRep)
        End If
    End Sub
End Class
