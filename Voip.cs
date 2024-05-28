using Godot;
using System;

public partial class Voip : Node {
    [Export] private AudioStreamPlayer input;
    [Export] private AudioStreamPlayer output;

    private AudioEffectCapture effect;
    private AudioStreamGeneratorPlayback playback;

    public override void _Ready() {
        effect = AudioServer.GetBusEffect(AudioServer.GetBusIndex("Microphone"), 0) as AudioEffectCapture;
        playback = output.GetStreamPlayback() as AudioStreamGeneratorPlayback;
    }

    public override void _Process(double delta) {
        int bufferSize = 512;

        if (effect.CanGetBuffer(bufferSize) && playback.CanPushBuffer(bufferSize)) {
            playback.PushBuffer(effect.GetBuffer(bufferSize));
        }

        effect.ClearBuffer();
    }
}
