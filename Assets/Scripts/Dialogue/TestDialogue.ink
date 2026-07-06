-> testStart

=== testStart ===
Hey this is a test dialogue.
This is a second line.
 + [Yes]
    -> chosen("Yes")
 + [No]
    -> chosen("No")

=== chosen(answer) ===
You chose {answer}!
-> END