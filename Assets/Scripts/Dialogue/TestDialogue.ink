-> testStart

=== testStart ===
Hey this is a test dialogue. #speaker: Milbert #visual: Milbert #layout: leftLayout
This is a second line.
 + [Yes]
    -> chosen("Yes")
 + [No]
    -> chosen("No")

=== chosen(answer) ===
You chose {answer}! #speaker: Hermbert #visual: Hermbert #layout: rightLayout
-> END