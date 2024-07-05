Adds `AwardRibbon` and `RevokeRibbon` behaviours thats grants kebals Final Frontier's ribbons upon triggering.

```
BEHAVIOUR
{
    name = AwardRibbon
    type = AwardRibbon

    // State that should trigger the behaviour.
    //
    // Type:      TriggeredBehaviour.State
    // Required:  Yes
    // Values:
    //     CONTRACT_ACCEPTED
    //     CONTRACT_FAILED
    //     CONTRACT_SUCCESS
    //     CONTRACT_COMPLETED
    //     PARAMETER_COMPLETED
    //
    onState = PARAMETER_COMPLETE

    // When the onState attribute is set to PARAMETER_COMPLETED, a value
    // must also be supplied for the parameter attribute. This is the name
    // of the parameter that we are checking for completion. This can be
    // specified multiple times.
    //
    // Type:      string
    // Required:  Sometimes (multiples allowed)
    //
    parameter = MyParameter

    // Kerbal(s) to award ribbons to.
    //
    // Type:      Kerbal
    // Required:  Yes (multiples allowed)
    //
    kerbal = Bob Kerman

    // Code of the ribbon to award. You can see ribbon codes in the FF's config.
    // Note: it seems that some ribbons (like celestial body related achievement)
    // can't be awarded this way. Just use custom ones.
    //
    // Type:      string
    // Required:  Yes (multiples allowed)
    //
    ribbon = X100
}
```

`RevokeRibbon` is analogous.
