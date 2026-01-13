Public Interface iMedia
    Property Hash As String
    Property Mime As MimeCods
    Property Length As Double
    Property Size As Size
    Property HRes As Integer
    Property VRes As Integer
    Property Pags As Integer
    <JsonIgnore> Property Thumbnail As Image
    Property Fch As Date

End Interface
