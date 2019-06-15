using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField]
    private AudioClip _arrow;
    [SerializeField]
    private AudioClip _death;
    [SerializeField]
    private AudioClip _fireball;
    [SerializeField]
    private AudioClip _gameOver;
    [SerializeField]
    private AudioClip _hit;
    [SerializeField]
    private AudioClip _level;
    [SerializeField]
    private AudioClip _newGame;
    [SerializeField]
    private AudioClip _rock;
    [SerializeField]
    private AudioClip _towerBuilt;

    public AudioClip Arrow {
        get {
            return _arrow;
        }
    }

    public AudioClip Death
    {
        get
        {
            return _death;
        }
    }

    public AudioClip Fireball
    {
        get
        {
            return _fireball;
        }
    }

    public AudioClip GameOver
    {
        get
        {
            return _gameOver;
        }
    }

    public AudioClip Hit
    {
        get
        {
            return _hit;
        }
    }

    public AudioClip Level
    {
        get
        {
            return _level;
        }
    }

    public AudioClip NewGame
    {
        get
        {
            return _newGame;
        }
    }

    public AudioClip Rock
    {
        get
        {
            return _rock;
        }
    }

    public AudioClip TowerBuilt
    {
        get
        {
            return _towerBuilt;
        }
    }
}
