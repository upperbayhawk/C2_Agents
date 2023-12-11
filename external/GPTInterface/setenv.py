import os

def set_env_variable(key, value):
    os.environ[key] = value
    print(f"Environment variable '{key}' set to '{value}'")

if __name__ == "__main__":
    set_env_variable("OPENAI_API_KEY", "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")

setx OPENAI_API_KEY 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

#self.channel_key = 'USI884Q84ANQ0HBX'
