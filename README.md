# Rimworld mod: Smoother Healing

This is a trivial transpiler mod that find-and-replaces two constants in Pawn\_HealthTracker.HealthTickInterval:

- 600 (ticks per check) constant with 60
- 0.01f (healing per check) with 0.001f

The net effect is to check for healing 10x more frequently, but make the overall healing the same.
This de-optimization is vaguely helpful when you have pawns with a very large number of very small injuries, such that the healing done in a single vanilla check is much larger than the size of the injuries.
