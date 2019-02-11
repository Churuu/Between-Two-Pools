using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Jesper Li 07/02 - 19
public class Motion
{
    private Vector3 source;
    private Vector3 target;
    private float initialVelocity;
    private float acceleration;
    private float time;
    private float tolerance;

    public Motion(Vector3 source, Vector3 target, float initialVelocity, float acceleration, float time, float tolerance)
    {
        this.source = source;
        this.target = target;
        this.initialVelocity = initialVelocity;
        this.acceleration = acceleration;
        this.time = time;
        this.tolerance = tolerance;
    }

    public Vector3 Source
    {
        get
        {
            return source;
        }
        set
        {
            source = value;
        }
    }

    public Vector3 Target
    {
        get
        {
            return target;
        }
        set
        {
            target = value;
        }
    }
    public float InitialVelocity
    {
        get
        {
            return initialVelocity;
        }
        set
        {
            initialVelocity = value;
        }
    }

    public float Acceleration
    {
        get
        {
            return acceleration;
        }
        set
        {
            acceleration = value;
        }
    }

    public float Time
    {
        get
        {
            return time;
        }
        set
        {
            time = value;
        }
    }

    public float Tolerance
    {
        get
        {
            return tolerance;
        }
        set
        {
            tolerance = value;
        }
    }

    public bool Valid
    {
        get
        {
            return tolerance * tolerance < (source - target).sqrMagnitude;
        }
    }

    private Vector3 SourceToTargetImp
    {
        get
        {
            return target - source;
        }
    }

    public Vector3 SourceToTarget
    {
        get
        {
            return Valid ? SourceToTargetImp : Vector3.zero;
        }
    }

    private Vector3 TargetToSourceImp
    {
        get
        {
            return source - target;
        }
    }

    public Vector3 TargetToSource
    {
        get
        {
            return Valid ? TargetToSourceImp : Vector3.zero;
        }
    }

    private Vector3 ForwardImp
    {
        get
        {
            return SourceToTargetImp.normalized;
        }
    }

    public Vector3 Forward
    {
        get
        {
            return Valid ? ForwardImp : Vector3.zero;
        }
    }

    private Vector3 BackwardImp
    {
        get
        {
            return TargetToSourceImp.normalized;
        }
    }

    public Vector3 Backward
    {
        get
        {
            return Valid ? BackwardImp : Vector3.zero;
        }
    }

    private float VelocityImp
    {
        get
        {
            return initialVelocity + time * acceleration;
        }
    }

    public float Velocity
    {
        get
        {
            return Valid ? VelocityImp : 0.0f;
        }
    }

    private Vector3 CurrentImp
    {
        get
        {
            return source + (time * initialVelocity + 0.5f * time * time * acceleration) * ForwardImp;
        }
    }

    public Vector3 Current
    {
        get
        {
            return Valid ? CurrentImp : source;
        }
    }

    private Vector3 SourceToCurrentImp
    {
        get
        {
            return CurrentImp - source;
        }
    }

    public Vector3 SourceToCurrent
    {
        get
        {
            return Valid ? SourceToCurrentImp : Vector3.zero;
        }
    }

    private Vector3 TargetToCurrentImp
    {
        get
        {
            return CurrentImp - target;
        }
    }

    public Vector3 TargetToCurrent
    {
        get
        {
            return Valid ? TargetToCurrentImp : Vector3.zero;
        }
    }

    private bool InSourceRegionImp
    {
        get
        {
            return Vector3.Dot(SourceToCurrentImp, SourceToTargetImp) < 0.0f;
        }
    }

    public bool InSourceRegion
    {
        get
        {
            return Valid ? InSourceRegionImp : true;
        }
    }

    private bool InTargetRegionImp
    {
        get
        {
            return Vector3.Dot(TargetToCurrentImp, TargetToSourceImp) < 0.0f;
        }
    }

    public bool InTargetRegion
    {
        get
        {
            return Valid ? InTargetRegionImp : true;
        }
    }

    public bool InMiddleRegion
    {
        get
        {
            return Valid ? !InSourceRegionImp && !InTargetRegionImp : true;
        }
    }
}
